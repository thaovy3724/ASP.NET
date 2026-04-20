namespace StoreApp.Core.Exceptions
{
    public class ProductCannotBeOrderedException(string message) : DomainException(message);
}