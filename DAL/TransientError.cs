using chronos.DAL.Interfaces;

namespace chronos.DAL;

public class TransientError : ITransientError
{
    public TransientErrors Error
    {
        get => (TransientErrors) Errno;
        set => Errno = (int) value;
    }
    public int Errno { get; set; }
}