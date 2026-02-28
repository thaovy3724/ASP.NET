namespace StoreApp.Core.Exceptions
{
    public class InsufficientStockException(string message) : DomainException(message);
}
