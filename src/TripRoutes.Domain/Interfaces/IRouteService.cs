using TripRoutes.Domain.Dtos;
using TripRoutes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripRoutes.Domain.Interfaces
{
    public interface IRouteService
    {
        Task<bool> AddRoute(RouteRequest route);
        Task<TripPathsResponse> GetPossiblePaths(string departure, string arrival);
        Task<string> GetCheaperPath(string departure, string arrival);
    }
}
