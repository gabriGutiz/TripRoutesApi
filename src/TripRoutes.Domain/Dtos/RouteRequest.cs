using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripRoutes.Domain.Dtos
{
    public class RouteRequest
    {
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public decimal Cost { get; set; }
    }
}
