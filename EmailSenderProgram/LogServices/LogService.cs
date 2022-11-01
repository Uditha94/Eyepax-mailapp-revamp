
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.LogServices
{
    public class LogService
    {
        private  Logger _logger;

        public enum LoggerLevel : int
        {
            Trace = 1,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        private  NLog.Logger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetCurrentClassLogger();
                return _logger;
            }
        }

        public  DateTime GetSystemDateTime()
        {
            DateTime utcDate = DateTime.UtcNow;
            return utcDate.AddHours(5.5);
        }

        public  void WriteLogMessage(string message, string userID, string token, LoggerLevel logLevel)
        {
            try
            {
                userID = userID ?? "Missing";
                token = token ?? "Missing";
                string header = GetSystemDateTime().ToString("dd-MM-yyyy hh:mm:ss:ms").PadRight(24, '0') + " | Service | " + "User ID: " + (userID == "Missing" ? userID : userID.PadRight(36, '0')) + " | " + "Session ID:" + token.PadRight(14, ' ') + " | ";
                message = header + message + Environment.NewLine;

                File.AppendAllText($"Logs/{DateTime.Now.ToString("yyyyMMdd")}.txt", message);

                switch (logLevel)
                {
                    case LoggerLevel.Trace:
                        Logger.Trace(message);
                        break;
                    case LoggerLevel.Debug:
                        Logger.Debug(message);
                        break;
                    case LoggerLevel.Info:
                        Logger.Info(message);
                        break;
                    case LoggerLevel.Warn:
                        Logger.Warn(message);
                        break;
                    case LoggerLevel.Error:
                        Logger.Error(message);
                        break;
                    case LoggerLevel.Fatal:
                        Logger.Fatal(message);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
