namespace chronos.DAL.Interfaces;

public interface ITransientError
{
    public int Errno { get; set; }
}

// documentation
// -1 - no transient error
// 0 - none error
// 1 - timeout