namespace chronos.DAL.Interfaces;

public interface IUpdateRepository<T> where T: class
{
    Task Update(T model, ITransientError error, CancellationToken ct = default);
}

