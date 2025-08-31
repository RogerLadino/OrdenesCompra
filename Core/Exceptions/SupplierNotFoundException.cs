namespace Domain.Exceptions;

public class SupplierNotFoundException(string message) : Exception(message)
{
}