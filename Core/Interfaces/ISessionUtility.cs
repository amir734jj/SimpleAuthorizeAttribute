using Microsoft.AspNetCore.Http;

namespace SimpleAuthorizeAttribute.Core.Interfaces
{
    public interface ISessionUtility
    {
        IUserInfo GetUserInfo(ISession session);

        bool IsAuthenticated(ISession session);
    }
}