namespace chronos.DAL.Interfaces;

public interface IAddRangeRepository<T> where T: class
{
    Task<long> AddRangeAsync(IEnumerable<T> models, ITransientError error, CancellationToken ct = default);
}

