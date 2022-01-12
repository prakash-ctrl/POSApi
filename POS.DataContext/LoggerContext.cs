using DataAccess;
using POS.Entity;
using POS.Entity.MasterEntity;
using POS.Utility;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace POS.DataContext
{
    public class LoggerContext:BaseContext
    {
        private readonly MsSqlConnect _connect;
        public LoggerContext(MsSqlConnect connect):base(connect)
        {
            _connect = connect;
        }


        public void DBLogger(LogEntity logEntity)
        {

            

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@LogTypeId", (int)logEntity.LogType));
            parameters.Add(new SqlParameter("@LogFrom", logEntity.LogFrom.DefaultDBNullValue()));
            parameters.Add(new SqlParameter("@CreatedBy", logEntity.UserId));
            parameters.Add(new SqlParameter("@ClientIPAddress", logEntity.ClientIPAddress));
            parameters.Add(new SqlParameter("@ServerIPAddress", logEntity.ServerIPAddress));
            parameters.Add(new SqlParameter("@StackTrace", logEntity.StackTrace.DefaultDBNullValue()));
            parameters.Add(new SqlParameter("@Exception", logEntity.Exception.DefaultDBNullValue()));
            parameters.Add(new SqlParameter("@InnerException", logEntity.InnerException.DefaultDBNullValue()));
            parameters.Add(new SqlParameter("@Message", logEntity.Message.DefaultDBNullValue()));
            _ = _connect.Execute("Save_ApplicationLogs", CommandType.StoredProcedure, parameters);
        }

        public List<LogTypeEntity> GetLoggerTypes()
        {
            return _connect.Execute<LogTypeEntity>("Get_ref_LogType", CommandType.StoredProcedure);
        }
    }
}
