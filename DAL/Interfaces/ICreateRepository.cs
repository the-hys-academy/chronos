using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface ICreateRepository<T> where T: class
{
    Task Create(T model, CancellationToken ct, out DbError? error);
}

