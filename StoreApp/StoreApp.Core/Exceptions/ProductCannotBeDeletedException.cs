namespace StoreApp.Core.Exceptions
{
    public class ProductCannotBeDeletedException(string message) : DomainException(message);
}
