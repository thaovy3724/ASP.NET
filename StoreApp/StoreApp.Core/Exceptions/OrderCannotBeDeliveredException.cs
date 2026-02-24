namespace StoreApp.Core.Exceptions
{
    public class OrderCannotBeDeliveredException(string message) : DomainException(message)
    {
    }
}
