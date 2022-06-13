namespace chronos.DAL.Models;

public class Region
{
    public long Uid { get; set; }
    public string Name { get; set; }
    public IEnumerable<(long, long)> Polygon;
}