namespace Domain.Exceptions;

public class ProductNameAlreadyExistsException(string message) : Exception(message)
{
}
