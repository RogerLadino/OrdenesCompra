namespace Domain.Exceptions;

public class CompanyNameAlreadyExistsException(string message) : Exception(message)
{
}
