using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IGetAllRepository<T> where T: class
{
    // Task<IEnumerable<T>> GetAllAsync(out DbError? error, CancellationToken ct = default);
    IAsyncEnumerable<T> GetAllAsync(ITransientError error, CancellationToken ct = default);
}
