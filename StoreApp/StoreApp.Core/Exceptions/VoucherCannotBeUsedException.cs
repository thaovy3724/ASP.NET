namespace StoreApp.Core.Exceptions
{
    public class VoucherCannotBeUsedException(string message) : DomainException(message)
    {
    }
}