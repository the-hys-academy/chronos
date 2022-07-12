namespace chronos.DAL.Interfaces;

public interface IAddRepository<T> where T: class
{
    Task<long> AddAsync(T model, ITransientError error, CancellationToken ct = default);
}

