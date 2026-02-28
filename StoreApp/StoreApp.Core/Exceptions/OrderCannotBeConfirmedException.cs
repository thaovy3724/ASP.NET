namespace StoreApp.Core.Exceptions
{
    public class OrderCannotBeConfirmedException(string message) : DomainException(message);
}
