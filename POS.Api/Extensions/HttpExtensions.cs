using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using POS.Api.Token;
using POS.Utility;
using System;
using System.Linq;


namespace POS.Api.Extensions
{
    public static class HttpExtensions
    {


        public static string GetClientIPAddress(this IHttpContextAccessor http)
        {
            return http.HttpContext.Connection.RemoteIpAddress?.ToString();
        }

        public static string GetServerIPAddress(this IHttpContextAccessor http)
        {
            return http.HttpContext.Features.Get<IHttpConnectionFeature>()?.LocalIpAddress?.ToString();
        }

        public static string GetCurrentUser(this IHttpContextAccessor http)
        {

            string currentUser = string.Empty;

            TokenManager tokenManager = (TokenManager)http.HttpContext.RequestServices.GetService(typeof(TokenManager));

            if (http.HttpContext.Request.Headers.ContainsKey(ApplicationConstants.AUTH_HEADER))
            {
                try
                {
                    var claimsPrinciple = tokenManager.DecryptToken(http.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == ApplicationConstants.AUTH_HEADER).Value);
                    currentUser = claimsPrinciple?.Identities.FirstOrDefault().Name;
                }
                catch(Exception ex)
                {
                    Logger.Log(ex.Message,Logger.TYPE.ERROR,LoggerExtensions.FormattedIPAddressNoUser, "HttpExtensions.GetCurrentUser");
                    currentUser = "[No User]";
                }
            }
            if (string.IsNullOrEmpty(currentUser))
            {
                return "[No User]";
            }

            return currentUser;

        }



    }
}
