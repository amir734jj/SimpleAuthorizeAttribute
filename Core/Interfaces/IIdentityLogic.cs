using System.Threading.Tasks;

namespace SimpleAuthorizeAttribute.Core.Interfaces
{
    public interface IIdentityLogic
    {
        Task<bool> Login(string username, string password, string role);

        Task<bool> Logout(string username);

        Task<bool> Validate(string username, string password);
        
        Task<bool> Validate(string username, string password, string currentRoleAccess, string requestedRoleAccess);
    }
}