namespace StoreApp.Infrastructure.Exceptions
{
    public class PaymentException(string message) : InfrastructureException(message);
}
