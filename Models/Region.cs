using NpgsqlTypes;

namespace chronos.Models{
    
    public class Region{
        
        public long ID { get; set; }  
        
        public string Name { get; set; }
        
        public NpgsqlPolygon Polygon { get; set;}

        public Region(string Name, NpgsqlPolygon Polygon) 
        {
            this.Name = Name;
            this.Polygon = Polygon;
        }

        public Region()
        {
        } 
    }
}