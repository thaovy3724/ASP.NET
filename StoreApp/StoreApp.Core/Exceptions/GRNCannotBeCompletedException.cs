namespace StoreApp.Core.Exceptions
{
    public class GRNCannotBeCompletedException(string message) : DomainException(message);
}
