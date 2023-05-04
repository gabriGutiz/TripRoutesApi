using TripRoutes.Domain.Interfaces;
using TripRoutes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripRoutes.Domain;

namespace TripRoutes.Data
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ConfigFile _config;

        public RouteRepository(ConfigFile config)
        {
            _config = config;
        }

        public async Task<bool> ValidateArrivalAsync(string arrival)
        {
            var database = await LoadDataBaseAsync();

            return (from r in database
                    where r.Arrival == arrival
                    select r).Any();
        }

        public async Task<bool> ValidateDepartureAsync(string departure)
        {
            var database = await LoadDataBaseAsync();

            return (from r in database
                    where r.Departure == departure
                    select r).Any();
        }

        public async Task<List<Route>> GetRoutesByDepartureAsync(string fromRoute)
        {
            var database = await LoadDataBaseAsync();

            var result = (from r in database
                          where r.Departure == fromRoute
                          select r).ToList();

            return result;
        }

        public async Task<bool> UpdateDatabaseAsync(Stream stream)
        {
            _ = stream ?? throw new ArgumentNullException(nameof(stream));

            if (stream.Length == 0)
            {
                return false;
            }

            using (var writer = File.AppendText($"{_config.OutputFile}\\{_config.FileName}"))
            {
                var builder = new StringBuilder();

                using (var reader = new StreamReader(stream))
                {
                    while (reader.Peek() >= 0)
                    {
                        try
                        {
                            var line = await reader.ReadLineAsync();
                            var routeFile = line.Split(',');

                            if (!(routeFile.Length == 3))
                            {
                                continue;
                            }

                            builder.AppendLine(line);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    writer.WriteLine(builder.ToString());
                }
            }
            return true;
        }

        private async Task<List<Route>> LoadDataBaseAsync()
        {
            var routes = new List<Route>();

            using (var fileStream = new FileStream($"{_config.OutputFile}\\{_config.FileName}", FileMode.Open))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = await reader.ReadLineAsync();
                        var routeFile = line.Split(',');

                        if (!(routeFile.Length == 3))
                        {
                            continue;
                        }

                        if (decimal.TryParse(routeFile[2], out var cost))
                        {
                            var route = new Route
                            {
                                Departure = routeFile[0],
                                Arrival = routeFile[1],
                                Cost = Convert.ToDecimal(routeFile[2])
                            };

                            routes.Add(route);
                        }
                    }
                }
            }
            return routes;
        }
    }
}
