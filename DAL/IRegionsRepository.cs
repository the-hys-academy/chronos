using chronos.DAL.Interfaces;
using chronos.DAL.Models;

namespace chronos.DAL;

public interface IRegionsRepository: IGetRepository<Region>, 
    IGetAllRepository<Region>, ICreateRepository<Region>, IUpdateRepository<Region>, IRemoveRepository 
{
    
}