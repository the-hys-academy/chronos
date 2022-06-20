using AutoMapper;
using chronos.DAL.Models;
using NpgsqlTypes;

namespace chronos.DAL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<Region.GeoPoint, NpgsqlPoint>()
        //     .ForMember(d => d.X, o
        //         => o.MapFrom(s => s.Latitude))
        //     .ForMember(d => d.Y, o
        //     => o.MapFrom(s => s.Longitude));
    }

}