using AutoMapper;
using System.Text;
using TripRoutes.Domain.Dtos;

namespace TripRoutes.Domain.Models.Mappers
{
    public class PathMapper : Profile
    {
        public PathMapper()
        {
            CreateMap<TripPath, TripPathResponse>()
                .ForMember(
                    dest => dest.TripPath,
                    opt => opt.MapFrom(src => src.Routes.ToString())
                    )
                .ForMember(
                    dest => dest.Cost,
                    opt => opt.MapFrom(src => src.Cost)
                    );
        }
    }
}
