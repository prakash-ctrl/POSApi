using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using POS.Api.Extensions;
using POS.Api.Token;
using POS.Utility;
using System;
using System.Linq;

namespace POS.Api.Filter
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        

       
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (!SkipAuthorization(context))
            {

                TokenManager tokenManager = (TokenManager)context.HttpContext.RequestServices.GetService(typeof(TokenManager));
                var result = true;
                if (!context.HttpContext.Request.Headers.ContainsKey(ApplicationConstants.AUTH_HEADER))
                {
                    Logger.Log("AUTH_HEADER not found!", Logger.TYPE.ERROR, LoggerExtensions.FormattedIPAddressNoUser, "TokenAuthenticationFilter.OnAuthorization");
                    result = false;
                }
                var token = string.Empty;
                if (result)
                {
                    token = context.HttpContext.Request.Headers.First(x => x.Key == ApplicationConstants.AUTH_HEADER).Value;
                    try
                    {
                        var claimsPrinciple = tokenManager.VerifyToken(token);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"You are not authorized! {ex.Message}", Logger.TYPE.ERROR, LoggerExtensions.FormattedIPAddressNoUser, "TokenAuthenticationFilter.OnAuthorization");
                        context.ModelState.AddModelError("Unauthorized", "You are not authorized!");
                    }
                }

                if (!result)
                {
                    context.ModelState.AddModelError("Unauthorized", "You are not authorized!");
                    context.Result = new UnauthorizedObjectResult(context.ModelState);
                }
            }
        }

        public bool SkipAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            return context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        }
    }
}
