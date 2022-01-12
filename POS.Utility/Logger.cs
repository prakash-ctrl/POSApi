using System;


namespace POS.Utility
{
    public class Logger
    {
        public static string LoggerPath = string.Empty;
        public enum TYPE { INFO=1,SUCCESS=2,WARNING=3,ERROR=4,LOG=5}
      


        public static void Log(string message)
        {
            Logger.Log(message, Logger.TYPE.INFO);
        }

        public static void Log(string message,Logger.TYPE logType)
        {
            Logger.Log(message, logType, string.Empty, string.Empty);
        }

        public static void Log(string message,Logger.TYPE logType,string IPAddress,string logFrom)
        {
            if (string.IsNullOrEmpty(Logger.LoggerPath))
                throw new Exception("Configure Logger.LoggerPath for logs!");

            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string fileExtension =logType switch
            {
                TYPE.SUCCESS=>".success",
                TYPE.ERROR=>".error",
                TYPE.WARNING=>".warning",
                TYPE.INFO=>".info",
                _=>".log"
            };

            string filePath = LoggerPath;
            if (!filePath.EndsWith(@"\"))
                filePath += @"\";
            string fullPath = string.Empty;
            string logTemplate = "{0}\t{1}\t{2}\t{3}\t{4}" + Environment.NewLine;
            string logFormattedData = string.Empty;
            try
            {
                fullPath=$"{filePath}{currentDate}{fileExtension}";
                logFormattedData = string.Format(logTemplate, (object)IPAddress, (object)DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), (object)logFrom, (object)logType.ToString(), (object)message);
                WriteLog(logFormattedData, fullPath);

            }
            catch (Exception ex)
            {
                fullPath = $"{filePath}{currentDate}.error";
                logFormattedData = string.Format(logTemplate, (object)IPAddress, (object)DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), (object)($"{logFrom} Error in Logger"), (object)Logger.TYPE.ERROR.ToString(), (object)ex.Message);
                WriteLog(logFormattedData, fullPath);
            }
            

        }

        private static void WriteLog(string content,string fileName)
        {
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fileName)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));
            }

            System.IO.File.AppendAllText(fileName, content);
        }
        
    }
}
