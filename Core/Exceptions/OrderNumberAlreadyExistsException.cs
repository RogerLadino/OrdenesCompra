namespace Domain.Exceptions;

public class OrderNumberAlreadyExistsException(string message) : Exception(message)
{
}
