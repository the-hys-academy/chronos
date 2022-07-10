namespace chronos.DAL.Interfaces;

public interface IRemoveRepository<T> where T: class
{
    Task<T> Remove(long id, ITransientError error, CancellationToken ct = default);
}

