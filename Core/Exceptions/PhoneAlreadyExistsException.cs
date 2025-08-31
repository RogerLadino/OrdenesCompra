namespace Domain.Exceptions;

public class PhoneAlreadyExistsException(string message) : Exception(message)
{
}