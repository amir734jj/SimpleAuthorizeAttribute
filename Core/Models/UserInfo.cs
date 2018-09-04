using SimpleAuthorizeAttribute.Core.Interfaces;

namespace SimpleAuthorizeAttribute.Core.Models
{
    /// <summary>
    /// UserInfo class
    /// </summary>
    public class UserInfo : IUserInfo
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}