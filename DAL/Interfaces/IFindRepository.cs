using System.Linq.Expressions;

namespace chronos.DAL.Interfaces;

public interface IFindRepository<T> where T: class
{
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, ITransientError error,
        CancellationToken ct = default);
}

