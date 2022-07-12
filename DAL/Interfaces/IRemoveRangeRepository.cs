namespace chronos.DAL.Interfaces;

public interface IRemoveRangeRepository<T> where T: class
{
    Task<T?> RemoveRangeAsync(IEnumerable<long> ids, ITransientError error, CancellationToken ct = default);
}

