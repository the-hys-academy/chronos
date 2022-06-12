using chronos.Models;

namespace chronos.DAL
{    
  public enum ErrorType {Ok, NoConnect, ClientError, Timeout, somethingelse};

  public interface IRegionRepository
  {
    Task CreateRegionRecord(string Name,            
                            IEnumerable<(long lat, long lon)> Polygon,
                            out ErrorType errorState,
                            CancellationToken ct = default); 
      
    Task UpdateRegionRecord(long id,           
                            string Name, 
                            IEnumerable<(long lat, long lon)> Polygon, 
                            out ErrorType errorState, 
                            CancellationToken ct = default);
        
    Task DeleteRegionRecord(long id,
                            out ErrorType errorState, 
                            CancellationToken ct = default);
        
    Task<Region> GetRegionSingleRecord(long id,
                                      out ErrorType errorState, 
                                      CancellationToken ct = default);
      
    Task<List<Region>> GetRegionRecords();
  }
}
