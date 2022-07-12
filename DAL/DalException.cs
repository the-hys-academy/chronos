using System.Data.Common;

namespace chronos.DAL;

public class DalException : DbException
{
    public DalException() {}
    
    public DalException(string? message, Exception? innerException)
        : base(message, innerException) {}
    
    public DalException(string? message)
        : base(message) { }
}