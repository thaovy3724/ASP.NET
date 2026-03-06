namespace StoreApp.Core.Exceptions
{
    public class GRNCannotBeUpdatedException(string message) : DomainException(message);
}
