using SimpleAuthorizeAttribute.Core.Interfaces;

namespace SimpleAuthorizeAttribute.Core.Models
{
    public class SimpleAuthorizeInfo : ISimpleAuthorizeInfo
    {
        public string RedirectToUponNotAuthenticated { get; set; }
        
        public string RedirectToUponNotAuthorized { get; set; }
    }
}