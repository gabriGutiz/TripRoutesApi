using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TripRoutes.Domain.Models;

namespace TripRoutes.Domain.Dtos
{
    public class TripPathsResponse
    {
        public int Total { get; set; }
        public IEnumerable<string> Trips { get; set; }
    }
}
