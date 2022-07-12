namespace Draft
{
    public class Region
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public IEnumerable<(double longitude, double latitude)> Polygon { get; set; }
    }
}