namespace chronos.DAL.Interfaces;

public interface ICreateRepository<T> where T: class
{
    Task<long> Create(T model, ITransientError error, CancellationToken ct = default);
}

