using Microsoft.AspNetCore.Http;
using POS.BusinessLogic;
using POS.Entity;
using POS.Utility;
using System;
using System.Diagnostics;
using System.Reflection;


namespace POS.Api.Extensions
{
    public static class LoggerExtensions
    {
        private static IHttpContextAccessor _http;
        private static LoggerBL _loggerBL;
        public static void Configure(IHttpContextAccessor http,LoggerBL loggerBL)
        {
            _http = http;
            _loggerBL = loggerBL;
        }

        public static string FormattedIPAddress
        {
            get
            {

                return $"C-{_http.GetClientIPAddress()}/S-{_http.GetServerIPAddress()}/#{_http.GetCurrentUser()}";
            }
        }

        public static string FormattedIPAddressNoUser
        {
            get
            {

                return $"C-{_http.GetClientIPAddress()}/S-{_http.GetServerIPAddress()}/#[No User]";
            }
        }

        public static void FileLogger(this Logger logger,string message,Logger.TYPE logType)
        {
            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            Logger.Log(message, logType,FormattedIPAddress ,$"{className}.{methodName}");
        }

        public static void FileLogger(this Logger logger,Exception exception)
        {
            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            LogEntity logEntity = new LogEntity();
            logEntity.UserId = _http.GetCurrentUser();
            logEntity.Exception = exception != null ? exception.Message : null;
            logEntity.InnerException = exception.InnerException != null ? exception.InnerException.Message : null;
            logEntity.ClientIPAddress = _http.GetClientIPAddress();
            logEntity.ServerIPAddress = _http.GetServerIPAddress();
            Logger.Log($"{logEntity.Exception}   {logEntity.InnerException}", Logger.TYPE.ERROR, FormattedIPAddress, $"{className}.{methodName}");
        }


        public static void DBLogger(this Logger logger,string message,Logger.TYPE logType)
        {
            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            LogEntity logEntity = new LogEntity
            {
                UserId = _http.GetCurrentUser(),
                ClientIPAddress = _http.GetClientIPAddress(),
                ServerIPAddress = _http.GetServerIPAddress(),
                Message = message,
                LogFrom = $"{className}.{methodName}",
                LogType=logType
            };
            _loggerBL.SaveLogs(logEntity);
        }

        public static void DBLogger(this Logger logger,Exception exception)
        {

            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            LogEntity logEntity = new LogEntity();
            logEntity.UserId = _http.GetCurrentUser();
            logEntity.Exception = exception!=null?exception.Message:null;
            logEntity.InnerException = exception.InnerException != null ? exception.InnerException.Message : null;
            logEntity.ClientIPAddress = _http.GetClientIPAddress();
            logEntity.ServerIPAddress = _http.GetServerIPAddress();
            logEntity.LogType = Logger.TYPE.ERROR;
            logEntity.LogFrom = $"{className}.{methodName}";
            _loggerBL.SaveLogs(logEntity);
        }

        public static void FileWithDBLogger(this Logger logger,string message,Logger.TYPE logType)
        {
            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            FileWithDBLogger(logger, message, logType, $"{className}.{methodName}");
            
        }

        public static void FileWithDBLogger(this Logger logger,Exception exception)
        {
            var stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            FileWithDBLogger(logger,exception,$"{className}.{methodName}");
        }

        private static void FileWithDBLogger(this Logger logger,string message,Logger.TYPE logType,string logFrom)
        {
           
            LogEntity logEntity = new LogEntity
            {
                UserId = _http.GetCurrentUser(),
                ClientIPAddress = _http.GetClientIPAddress(),
                ServerIPAddress = _http.GetServerIPAddress(),
                Message = message,
                LogFrom =logFrom,
                LogType = logType
            };
            Logger.Log(message, logType,FormattedIPAddress,logFrom);
            _loggerBL.SaveLogs(logEntity);
        }

        private static void FileWithDBLogger(this Logger logger,Exception exception,string logFrom)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.UserId = _http.GetCurrentUser();
            logEntity.Exception = exception != null ? exception.Message : null;
            logEntity.InnerException = exception.InnerException != null ? exception.InnerException.Message : null;
            logEntity.ClientIPAddress = _http.GetClientIPAddress();
            logEntity.ServerIPAddress = _http.GetServerIPAddress();
            logEntity.LogType = Logger.TYPE.ERROR;
            logEntity.LogFrom = logFrom;
            Logger.Log($"{logEntity.Exception}   {logEntity.InnerException}", Logger.TYPE.ERROR, FormattedIPAddress, logFrom);
            _loggerBL.SaveLogs(logEntity);
        }
    }
}
