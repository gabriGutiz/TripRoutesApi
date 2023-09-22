using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripRoutes.Domain.Exceptions
{
    public class TripRoutesException : Exception
    {
        public int CodigoHttp { get; set; }
        public Error Error { get; set; }
        public TripRoutesException(int codigoHttp, string message)
        {
            CodigoHttp = codigoHttp;
            Error = new Error
            {
                Message = message
            };
        }
    }
}
