using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleAuthorizeAttribute.Core.Interfaces;

namespace SimpleAuthorizeAttribute.Core.Attributes.Role
{
    public class UserRoleActionFilter : IAsyncActionFilter
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
        public UserRoleActionFilter(IIdentityLogic identityLogic, ISessionUtility sessionUtility, ISimpleAuthorizeInfo simpleAuthorizeInfo)
        {
            _identityLogic = identityLogic;
            _sessionUtility = sessionUtility;
            _simpleAuthorizeInfo = simpleAuthorizeInfo;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = (Controller) context.Controller;
            var method = ((ControllerActionDescriptor) context.ActionDescriptor).MethodInfo;
            
            var controllerLevelAuthorize = controller.GetType().GetCustomAttribute<UserRoleMiddlewareAttribute>();
            var actionLevelAuthorize = method.GetCustomAttribute<UserRoleMiddlewareAttribute>();

            if (controllerLevelAuthorize == null && actionLevelAuthorize == null)
            {
                await next();
            }
            else
            {
                var requestRole = controllerLevelAuthorize?.Role ?? actionLevelAuthorize.Role;

                // Try to get username/password from session
                var userInfo = _sessionUtility.GetUserInfo(context.HttpContext.Session);

                var result = await _identityLogic.Validate(userInfo.Username, userInfo.Password, userInfo.Role, requestRole);

                // Validate username/password
                if (result)
                {
                    await next();
                }
                else
                {
                    // Redirect to not-authenticated
                    context.HttpContext.Response.Redirect(_simpleAuthorizeInfo.RedirectToUponNotAuthorized);
                }
            }
        }
    }
}