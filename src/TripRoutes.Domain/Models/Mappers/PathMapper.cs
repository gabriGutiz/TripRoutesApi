using AutoMapper;
using System.Text;
using TripRoutes.Domain.Dtos;

namespace TripRoutes.Domain.Models.Mappers
{
    public class PathMapper : Profile
    {
        public PathMapper()
        {
            CreateMap<IEnumerable<TripPath>, TripPathsResponse>()
                .ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(src => src.Count())
                    )
                .ForMember(
                    dest => dest.Trips,
                    opt => opt.MapFrom(src => src.Select(value => value.ToString()))
                    );
        }
    }
}
