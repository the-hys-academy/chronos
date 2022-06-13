using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IGetRepository<T> where T: class
{
    Task<T> Get(long id, CancellationToken ct, out DbError? error);
}
