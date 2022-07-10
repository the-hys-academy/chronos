namespace chronos.DAL.Interfaces;

public interface IGetAllRepository<T> where T: class
{
    IAsyncEnumerable<T> GetAllAsync(ITransientError error, CancellationToken ct = default);
}
