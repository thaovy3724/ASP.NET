namespace StoreApp.Core.Exceptions
{
    public class InvalidStockQuantityException(string message) : DomainException(message);
}
