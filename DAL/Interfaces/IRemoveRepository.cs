namespace chronos.DAL.Interfaces;

public interface IRemoveRepository<T> where T: class
{
    Task<T?> RemoveAsync(long id, ITransientError error, CancellationToken ct = default);
}

