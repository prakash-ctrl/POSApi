using System.Collections.Generic;

namespace POS.Utility
{
    public static class ApplicationConstants
    {
        
        public static string POS_CONNECTION_STRING = "";
        public static string APP_NAME = "";
        public static string SYMMETRIC_KEY = "";
        public static readonly string AUTH_HEADER = "auth";
        public static string LOGGER_PATH = "LogPath";
        public static readonly string LOG_TYPE = "logType";
        public static int TOKEN_EXPIRATION_HOURS = 1;
        public static readonly string DEFAULT_CORS_POLICY="DefaultCorsPolicy";
        public static IEnumerable<string> Origins;


    }
}
