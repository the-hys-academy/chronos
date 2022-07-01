using chronos.Models;

namespace chronos.DAL
{  
    public class Errors
    {
     public int ErrorNo { get; set; }
    }

  
  public interface IRegionRepository
  {
    Task<long> Create(Region region,            
                Errors error,
                CancellationToken ct = default); 
      
    Task Update(Region region,     
                Errors error,
                CancellationToken ct = default);
        
    Task<Region> Delete(long id,
                        Errors error,
                        CancellationToken ct = default);
        
    Task<Region> Get( long id,
                      Errors error, 
                      CancellationToken ct = default);
      
    Task<IEnumerable<Region>> Get(Errors error, 
                                  CancellationToken ct = default);
  }
}
