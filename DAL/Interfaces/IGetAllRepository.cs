using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IGetAllRepository<T> where T: class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct, out DbError? error);
}
