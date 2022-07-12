namespace chronos.DAL.Interfaces;

public interface IUpdateRepository<T> where T: class
{
    Task UpdateAsync(T model, ITransientError error, CancellationToken ct = default);
}

