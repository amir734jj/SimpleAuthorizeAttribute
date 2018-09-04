namespace SimpleAuthorizeAttribute.Core.Interfaces
{
    public interface IUserInfo
    {
        string Username { get; set; }
        
        string Password { get; set; }
        
        string Role { get; set; }
    }
}