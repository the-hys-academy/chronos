
using Npgsql;
using System.Data.Common;

public interface IRegionRepository
    {

        Task<int?> Create(Region reg,
                    Error err,
                    CancellationToken ct = default);
        /*Task<Region> Get(long id,
                    out Error err,
                    CancellationToken ct = default);*/
        /*Task Update(Region Name, 
                    Error err,
                    CancellationToken ct = default);

        Task <Region>Delete(long id,
                    Error err,
                    CancellationToken ct=default);*/

        //IAsyncEnumerable<Region> RetRegionRecords(CancellationToken ct=default);   
    }


        
