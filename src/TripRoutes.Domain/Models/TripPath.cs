using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripRoutes.Domain.Models
{
    public class TripPath
    {
        public IEnumerable<Route> Routes { get; set; }
        public decimal Cost { get; set; } = 0;

        public void AddRoute(Route route)
        {
            if (Routes is null)
            {
                Routes = new List<Route>();
            }
            Routes = Routes.Append(route);
            Cost += route.Cost;
        }

        public void SumPaths(TripPath path)
        {
            Routes = Routes.Concat(path.Routes);
            Cost += path.Cost;
        }
    }
}
