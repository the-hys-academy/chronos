using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IRemoveRepository
{
    Task Remove(long id, out DbError? error, CancellationToken ct = default);
}

