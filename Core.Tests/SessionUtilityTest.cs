using System.Collections.Generic;
using AutoFixture;
using SimpleAuthorizeAttribute.Core.Models;
using SimpleAuthorizeAttribute.Core.Utilities;
using Xunit;

namespace Core.Tests
{
    public class SessionUtilityTest
    {
        private readonly Fixture _fixture;

        public SessionUtilityTest()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Test__GetUserInfo()
        {
            // Arrange
            var userinfo = _fixture.Create<UserInfo>();
            
            var (username, password, role, authenticated) =
                (_fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<KeyValuePair<string, string>>());
            
            var utility = new SessionUtility(username, password, role, authenticated);
            
            var session = new MockSession(new Dictionary<string, string>
            {
                {username, userinfo.Username},
                {password, userinfo.Password},
                {role, userinfo.Role},
            });
            
            // Act
            var result = (UserInfo) utility.GetUserInfo(session);

            // Assert
            Assert.Equal(userinfo, result, new UserInfoEqualityComparer());
        }
    }

    public class UserInfoEqualityComparer : IEqualityComparer<UserInfo>
    {
        public bool Equals(UserInfo x, UserInfo y) => x.Username == y.Username
                                                      && x.Password == y.Password
                                                      && x.Role == y.Role;

        public int GetHashCode(UserInfo obj) => throw new System.NotImplementedException();
    }
}