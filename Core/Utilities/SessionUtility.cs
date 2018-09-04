using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SimpleAuthorizeAttribute.Core.Interfaces;
using SimpleAuthorizeAttribute.Core.Models;

namespace SimpleAuthorizeAttribute.Core.Utilities
{    
    public class SessionUtility : ISessionUtility
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _role;
        private readonly KeyValuePair<string, string> _authenticated;

        public SessionUtility(): this("username", "password", "role", new KeyValuePair<string, string>("authenticated", "authenticated")) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="authenticated"></param>
        public SessionUtility(string username, string password, string role, KeyValuePair<string, string> authenticated)
        {
            _username = username;
            _password = password;
            _role = role;
            _authenticated = authenticated;
        }

        /// <summary>
        /// Returns UserInfo from session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public virtual IUserInfo GetUserInfo(ISession session) => new UserInfo
        {
            Username = session.GetString(_username),
            Password = session.GetString(_password),
            Role = session.GetString(_role)
        };

        /// <summary>
        /// Check whether user is logged in or not
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public virtual bool IsAuthenticated(ISession session) => session.GetString(_authenticated.Key) == _authenticated.Value;
    }
}