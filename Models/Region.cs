
namespace chronos.Models{
    
    public class Region{
        
        public long ID { get; set; }  
        
        public string Name { get; set; }
        
        public IEnumerable<( float, float )> Polygon { get; set; }
    }
}