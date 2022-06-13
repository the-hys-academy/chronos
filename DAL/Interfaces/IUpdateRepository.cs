using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IUpdateRepository<T> where T: class
{
    Task Update(T model, CancellationToken ct, out DbError? error);
}

