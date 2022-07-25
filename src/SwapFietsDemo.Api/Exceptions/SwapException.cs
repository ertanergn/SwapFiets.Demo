namespace SwapFietsDemo.Api.Exceptions;

public class SwapException : Exception
{
    public SwapException(string message, Exception? innerException = null) : base(message, innerException) { }
}