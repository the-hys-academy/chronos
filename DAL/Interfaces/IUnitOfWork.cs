using chronos.DAL.Enums;

namespace chronos.DAL.Interfaces;

public interface IUnitOfWork
{
    // спорный момент, можно и не делать
    void SaveChangesAsync(CancellationToken ct, out DbError? error); 
}