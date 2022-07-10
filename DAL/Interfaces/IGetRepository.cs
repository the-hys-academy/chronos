namespace chronos.DAL.Interfaces;

public interface IGetRepository<T> where T: class
{
    Task<T> Get(long id, ITransientError error, CancellationToken ct = default);
}

