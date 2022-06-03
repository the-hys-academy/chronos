using chronos.Models;

namespace chronos.DAL
{
    public interface IRegionRepository
    {
      // Для дальнейших шагов по отработке ошибок методы должны вернуть статус удачно/нет? Или текст ошибки? Flag?
        Task<string> CreateRegionRecord(string Name,            //Task<(string, int)>
                                        IEnumerable<(long lat, long lon)> Polygon,
                                        CancellationToken ct = default); 
        
        Task<string> UpdateRegionRecord(int id,                 // for bug fixes
                                        string Name, 
                                        IEnumerable<(long lat, long lon)> Polygon, 
                                        CancellationToken ct = default);
        
        Task<string> DeleteRegionRecord(int id,
                                        CancellationToken ct = default);
        
        Task<(Region, string)> GetRegionSingleRecord(int id,
                                                   CancellationToken ct = default);
        
        Task<List<Region>> GetRegionRecords();
    }
}
