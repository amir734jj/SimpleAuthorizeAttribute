using System;

namespace SimpleAuthorizeAttribute.Core.Attributes.Role
{
    public class UserRoleMiddlewareAttribute : Attribute
    {
        public string Role { get; }
        
        public UserRoleMiddlewareAttribute(string role)
        {
            Role = role;
        }
    }
}