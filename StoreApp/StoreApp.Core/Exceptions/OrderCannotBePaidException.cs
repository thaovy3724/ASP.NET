namespace StoreApp.Core.Exceptions
{
    public class OrderCannotBePaidException(string message) : DomainException(message);
}
