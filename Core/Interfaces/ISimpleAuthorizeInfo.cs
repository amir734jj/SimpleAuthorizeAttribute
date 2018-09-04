namespace SimpleAuthorizeAttribute.Core.Interfaces
{
    public interface ISimpleAuthorizeInfo
    {
        string RedirectToUponNotAuthenticated { get; set; }
        
        string RedirectToUponNotAuthorized { get; set; }
    }
}