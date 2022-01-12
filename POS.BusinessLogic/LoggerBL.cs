using POS.DataContext;
using POS.Entity;
using POS.Entity.MasterEntity;
using System.Collections.Generic;

namespace POS.BusinessLogic
{
    public class LoggerBL
    {
        private readonly LoggerContext _logContext;
        public LoggerBL(LoggerContext logContext)
        {
            _logContext = logContext;
        }

        #region Save Logs
        public void SaveLogs(LogEntity log)
        {
            _logContext.DBLogger(log);
        }
        #endregion

        #region Get Log Types

        public List<LogTypeEntity> GetLoggerTypes()
        {
            return _logContext.GetLoggerTypes();
        }

        #endregion

    }
}
