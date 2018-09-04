namespace SimpleAuthorizeAttribute.Core.Models
{
    /// <summary>
    /// UserInfo Struct
    /// </summary>
    public struct UserInfo
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}