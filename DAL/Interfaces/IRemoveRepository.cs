using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IRemoveRepository
{
    Task Remove(long id, CancellationToken ct, out DbError? error);
}

