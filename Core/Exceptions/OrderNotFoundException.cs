namespace Domain.Exceptions;

public class OrderNotFoundException(string message) : Exception(message)
{
}
