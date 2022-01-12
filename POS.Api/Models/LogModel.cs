using POS.Utility;

namespace POS.Api.Models
{
    public class LogModel
    {
        public string UserId { get; set; }
        public string ClientIPAddress { get; set; }
        public string ServerIPAddress { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public Logger.TYPE LogType { get; set; }
    }
}
