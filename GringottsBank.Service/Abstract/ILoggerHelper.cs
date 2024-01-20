namespace GringottsBank.Service.Abstract
{
    public interface ILoggerHelper
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
