using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripRoutes.Domain.Models;

namespace TripRoutes.Domain.Interfaces
{
    public interface IRouteRepository
    {
        Task<bool> ValidateArrivalAsync(string arrival);

        Task<bool> ValidateDepartureAsync(string departure);

        Task<List<Route>> GetRoutesByDepartureAsync(string fromRoute);

        Task<bool> UpdateDatabaseAsync(Stream stream);
    }
}
