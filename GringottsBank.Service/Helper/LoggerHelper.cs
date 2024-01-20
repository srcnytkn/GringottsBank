using GringottsBank.Service.Abstract;
using Microsoft.Extensions.Logging;
using NLog;

namespace GringottsBank.Service.Helper
{
    public class LoggerHelper : ILoggerHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public void LogInformation(string message)
        {
            logger.Info(message);
        }

        public void LogWarning(string message)
        {
            logger.Warn(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
