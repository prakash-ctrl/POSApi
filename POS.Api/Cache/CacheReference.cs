
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using POS.BusinessLogic;
using POS.Entity.MasterEntity;
using POS.Utility;
using System;
using System.Collections.Generic;


namespace POS.Api.Cache
{
    public static class CacheReference
    {
        private static LoggerBL _loggerBL;
        private static IMemoryCache _cache;
        private static IHttpContextAccessor _http;


        public static void InjectService(LoggerBL loggerBL)
        {
            _loggerBL = loggerBL;
        }
        public static void InitializeCache()
        {
            List<LogTypeEntity> _logTypes = new List<LogTypeEntity>();
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };

            if(!_cache.TryGetValue(ApplicationConstants.LOG_TYPE,out List<LogTypeEntity> logTypes))
            {
                logTypes = _loggerBL.GetLoggerTypes();
                _cache.Set(ApplicationConstants.LOG_TYPE, logTypes, cacheEntryOptions);
            }
          
        }

       

        public static void ConfigureIMemoryCache(IMemoryCache cache,IHttpContextAccessor http)
        {
            _cache = cache;
            _http = http;
        }


        public static List<LogTypeEntity> GetLoggerTypes()
        {
            return _cache.Get<List<LogTypeEntity>>(ApplicationConstants.LOG_TYPE);
        }

        
    }
}
