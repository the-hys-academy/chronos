using chronos.DAL.Interfaces;

namespace chronos.DAL;

public class TransientErrorWrapper : ITransientError
{
    public TransientErrors Errno { get; set; }
}