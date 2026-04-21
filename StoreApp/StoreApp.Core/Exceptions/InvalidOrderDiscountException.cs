namespace StoreApp.Core.Exceptions
{
    public class InvalidOrderDiscountException(string message) : DomainException(message)
    {
    }
}