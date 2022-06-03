
namespace chronos.Models{
    
    public class Region{
        
        public int ID { get; set; }  // string or int ?
        
        public string Name { get; set; }
        
        public IEnumerable<( float, float )> Polygon { get; set; }
    }
}