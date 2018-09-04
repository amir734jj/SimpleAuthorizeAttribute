using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleAuthorizeAttribute.Core.Interfaces;

namespace SimpleAuthorizeAttribute.Core.Attributes.Authorize
{
    public class AuthorizeActionFilter : IAsyncActionFilter
    {
        private readonly IIdentityLogic _identityLogic;
        
        private readonly ISessionUtility _sessionUtility;
        
        private readonly ISimpleAuthorizeInfo _simpleAuthorizeInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identityLogic"></param>
        /// <param name="sessionUtility"></param>
        /// <param name="simpleAuthorizeInfo"></param>
        public AuthorizeActionFilter(IIdentityLogic identityLogic, ISessionUtility sessionUtility, ISimpleAuthorizeInfo simpleAuthorizeInfo)
        {
            _identityLogic = identityLogic;
            _sessionUtility = sessionUtility;
            _simpleAuthorizeInfo = simpleAuthorizeInfo;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = (Controller) context.Controller;
            var method = ((ControllerActionDescriptor) context.ActionDescriptor).MethodInfo;
            
            var controllerLevelAuthorize = controller.GetType().GetCustomAttribute<AuthorizeMiddlewareAttribute>();
            var actionLevelAuthorize = method.GetCustomAttribute<AuthorizeMiddlewareAttribute>();

            if (controllerLevelAuthorize != null || actionLevelAuthorize != null)
            {
                // Try to get username/password from session
                var userInfo = _sessionUtility.GetUserInfo(context.HttpContext.Session);

                var result = await _identityLogic.Validate(userInfo.Username, userInfo.Password);

                // Validate username/password
                if (result)
                {
                    await next();
                }
                else
                {
                    // Redirect to not-authenticated
                    context.HttpContext.Response.Redirect(_simpleAuthorizeInfo.RedirectToUponNotAuthenticated);
                }
            }
            else
            {
                // Basically allow all
                await next();
            }
        }
    }
}