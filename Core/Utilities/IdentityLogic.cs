using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAuthorizeAttribute.Core.Interfaces;

namespace SimpleAuthorizeAttribute.Core.Utilities
{
    public class IdentityLogic : IIdentityLogic
    {
        /// <summary>
        /// Holds all users
        /// </summary>
        private readonly List<IUserInfo> _allUserInfos;
        
        /// <summary>
        /// Holds all active users
        /// </summary>
        private readonly List<IUserInfo> _activeUserInfos = new List<IUserInfo>();

        /// <summary>
        /// Singleton padlock
        /// </summary>
        private static object _singletonPadlock;
        
        /// <summary>
        /// Constructor to inject all active users
        /// </summary>
        /// <param name="allUserInfos"></param>
        public IdentityLogic(List<IUserInfo> allUserInfos)
        {
            // Initialize the padlock
            if (_singletonPadlock != null)
            {
                throw new Exception("This class has to be instantiated once");
            }
            
            _allUserInfos = allUserInfos;
            _singletonPadlock = new object();
        }

        /// <summary>
        /// Handles login action
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public virtual async Task<bool> Login(string username, string password, string role)
        {
            var userInfo = _allUserInfos.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (userInfo == null) return false;
            
            // Add the user to list of active users
            _activeUserInfos.Add(userInfo);

            return true;
        }

        /// <summary>
        /// Handles logout action
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual async Task<bool> Logout(string username)
        {
            var userInfo = _activeUserInfos.FirstOrDefault(x => x.Username == username);

            if (userInfo == null)
            {
                return false;
            }

            _activeUserInfos.Remove(userInfo);

            return true;
        }

        /// <summary>
        /// Validates whether user is logged in or not
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual async Task<bool> Validate(string username, string password) =>
            _allUserInfos.Any(x => x.Username == username && x.Password == password);

        /// <summary>
        /// Validates whether user is logged in or not and checks whether request role is valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="currentRoleAccess"></param>
        /// <param name="requestedRoleAccess"></param>
        /// <returns></returns>
        public virtual async Task<bool> Validate(string username, string password, string currentRoleAccess, string requestedRoleAccess) =>
            _allUserInfos.Any(x => x.Username == username && x.Password == password && x.Role == currentRoleAccess && x.Role == requestedRoleAccess);
    }
}