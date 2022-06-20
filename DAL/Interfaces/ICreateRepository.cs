using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface ICreateRepository<T> where T: class
{
    Task<T> Create(T model, ITransientError error, CancellationToken ct = default);
}

