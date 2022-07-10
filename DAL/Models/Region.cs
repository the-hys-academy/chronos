namespace chronos.DAL.Models;

public class Region
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<GeoPoint> Polygon;

    public class GeoPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}