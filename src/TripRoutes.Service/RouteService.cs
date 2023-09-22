using TripRoutes.Domain.Dtos;
using TripRoutes.Domain.Interfaces;
using TripRoutes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripRoutes.Domain;
using TripRoutes.Domain.Exceptions;
using AutoMapper;

namespace TripRoutes.Service
{
    public class RouteService : IRouteService
    {
        private readonly ConfigFile _config;
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;
        public RouteService(
            IRouteRepository routeRepository,
            ConfigFile config,
            IMapper mapper
            )
        {
            _routeRepository = routeRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<bool> AddRoute(RouteRequest route)
        {
            // Implement AddRoute logic
            throw new NotImplementedException();
        }

        public async Task<TripPathsResponse> GetPossiblePaths(string departure, string arrival)
        {
            if (string.IsNullOrEmpty(departure) || string.IsNullOrEmpty(arrival))
            {
                throw new TripRoutesException(400, "Departure or arrival not found!");
            }
            if (!await _routeRepository.ValidateArrivalAsync(arrival))
            {
                throw new TripRoutesException(400, $"Arrival '{arrival}' not valid.");
            }
            if (!await _routeRepository.ValidateDepartureAsync(departure))
            {
                throw new TripRoutesException(400, $"Departure '{departure}' not valid.");
            }

            var destinies = await GetPossiblePathAsync(departure, arrival);

            if (destinies is null || destinies.Count() == 0)
            {
                return null;
            }

            destinies
                .Where(o => o.Routes != null || o.Routes.Count() != 0)
                .OrderBy(o => o.Cost);

            var response = _mapper.Map<TripPathsResponse>(destinies);

            return response;
        }

        public async Task<string> GetCheaperPath(string departure, string arrival)
        {
            if (string.IsNullOrEmpty(departure) || string.IsNullOrEmpty(arrival))
            {
                throw new TripRoutesException(400, "Departure or arrival not found!");
            }
            if (!await _routeRepository.ValidateArrivalAsync(arrival))
            {
                throw new TripRoutesException(400, $"Arrival '{arrival}' not valid.");
            }
            if (!await _routeRepository.ValidateDepartureAsync(departure))
            {
                throw new TripRoutesException(400, $"Departure '{departure}' not valid.");
            }

            var destinies = await GetPossiblePathAsync(departure, arrival);

            if (destinies is null || destinies.Count() == 0)
            {
                return null;
            }

            var destiny = destinies
                .Where(o => o.Routes != null || o.Routes.Count() != 0)
                .OrderBy(o => o.Cost)
                .FirstOrDefault();

            return destiny.ToString();
        }

        private async Task<IEnumerable<TripPath>> GetPossiblePathAsync(string departure, string arrival)
        {
            var response = new List<TripPath>();

            var routes = await _routeRepository.GetRoutesByDepartureAsync(departure);

            if (routes == null || routes.Count == 0)
            {
                return null;
            }

            foreach (Route route in routes)
            {
                var tripPath = new TripPath();
                tripPath.AddRoute(route);

                if (route.Arrival == arrival)
                {
                    response.Add(tripPath);
                    continue;
                }

                var newRoutes = await GetPossiblePathAsync(route.Arrival, arrival);

                if (newRoutes == null || newRoutes.Count() == 0)
                {
                    continue;
                }

                foreach (TripPath newRoute in newRoutes)
                {
                    TripPath newTripPath = new TripPath
                    {
                        Cost = tripPath.Cost,
                        Routes = tripPath.Routes
                    };
                    newTripPath.SumPaths(newRoute);
                    response.Add(newTripPath);
                }
            }
            return response;
        }
    }
}
