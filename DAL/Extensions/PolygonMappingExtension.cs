using chronos.DAL.Models;
using NpgsqlTypes;

namespace chronos.DAL.Extensions;

public static class PolygonMappingExtension
{
    public static NpgsqlPolygon ToNpgsql(this IEnumerable<Region.GeoPoint> polygon)
    {
        return new NpgsqlPolygon(polygon.Select(x => new NpgsqlPoint(x.Latitude, x.Longitude)));
    }

    public static IEnumerable<Region.GeoPoint> ToModel(this NpgsqlPolygon polygon)
    {
        return polygon.Select(x => new Region.GeoPoint { Latitude = x.X, Longitude = x.Y });
    }
}