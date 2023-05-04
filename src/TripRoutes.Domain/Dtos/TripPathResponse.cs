using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TripRoutes.Domain.Dtos
{
    public class TripPathResponse
    {
        public string TripPath { get; set; }
        public decimal Cost { get; set; }

        public override string ToString()
        {
            return $"{TripPath} with cost **${Cost}**";
        }
    }
}
