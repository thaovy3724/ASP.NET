namespace StoreApp.Core.Exceptions
{
    public class OrderCannotBeCanceledException(string message) : DomainException(message);
}
