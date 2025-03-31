namespace Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message) { }
}