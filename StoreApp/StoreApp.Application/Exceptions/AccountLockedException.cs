namespace StoreApp.Application.Exceptions
{
    public class AccountLockedException(string message)
        : ApplicationException(message, (System.Net.HttpStatusCode)423);
}