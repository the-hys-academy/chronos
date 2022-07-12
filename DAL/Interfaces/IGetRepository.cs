namespace chronos.DAL.Interfaces;

public interface IGetRepository<T> where T: class
{
    Task<T?> GetAsync(long id, ITransientError error, CancellationToken ct = default);
}

