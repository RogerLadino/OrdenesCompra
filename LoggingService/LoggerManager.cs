using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingService
{
    public class LoggerManager(ILogger logger) : ILoggerManager
    {
        public void LogDebug(string message) =>
            logger.Debug(message);

        public void LogError(string message) =>
            logger.Error(message);

        public void LogInformation(string message) =>
            logger.Information(message);

        public void LogWarning(string message) =>
            logger.Warning(message);
    }
}
