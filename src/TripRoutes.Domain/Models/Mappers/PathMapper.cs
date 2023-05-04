using AutoMapper;
using TripRoutes.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripRoutes.Domain.Models.Mappers
{
    public class PathMapper : Profile
    {
        public PathMapper()
        {
            // TODO: Implement mapper to TripPath
            CreateMap<TripPath, TripPathResponse>()
                .ForMember(
                    dest => dest.Cost,
                    opt => opt.MapFrom(src => src.Cost)
                    );
        }
    }
}
