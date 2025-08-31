namespace Domain.Exceptions;

public class CustomerNotFoundException(string message) : Exception(message)
{
}
