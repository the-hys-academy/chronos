using chronos.DAL.Interfaces;
using chronos.DAL.Models;

namespace chronos.DAL;

using T = Region;

public interface IRegionsRepository: IGetRepository<T>, 
    IGetAllRepository<T>, ICreateRepository<T>, IUpdateRepository<T>, IRemoveRepository
{
    
}