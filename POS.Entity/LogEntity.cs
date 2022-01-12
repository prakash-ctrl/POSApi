using POS.Utility;

namespace POS.Entity
{
    public class LogEntity
    {
        public string UserId { get; set; }
        public string ClientIPAddress { get; set; }
        public string ServerIPAddress { get; set; }
        public string StackTrace { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public string LogFrom { get; set; }
        public Logger.TYPE LogType { get; set; }
        

    }
}
