using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripRoutes.Domain.Dtos;

namespace TripRoutes.Domain.Models.Mappers
{
    public class RouteMapper : Profile
    {
        public RouteMapper()
        {
            CreateMap<RouteRequest, Route>();
        }
    }
}
