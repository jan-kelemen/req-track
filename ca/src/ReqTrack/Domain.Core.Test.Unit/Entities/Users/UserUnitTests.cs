using System;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using Xunit;

namespace ReqTrack.Domain.Core.Test.Unit.Entities.Users
{
    public class UserUnitTests
    {
        [Fact]
        public void UserConstructorArgumentValidationWorks()
        {
            var validUserName = "user1";
            var invalidUserName = new string('u', 51);

            var validDisplayName = "display1";
            var invalidDisplayName = new string('d', 101);

            var validPassword = UserValidationHelper.HashPassword("abc123");
            var invalidPassword1 = "abc123";
            var invalidPassword2 = new string('p', 33);

            var validProjects = new UserProjects(new[]
            {
                new BasicProject("id2", "name1"),
                new BasicProject("id3", "name2"),
            });
            var invalidProjects = new UserProjects(new[]
            {
                new BasicProject("id4", "name3"),
                new BasicProject("id5", "name3"),
            });

            Assert.Throws<ArgumentException>(() =>
                new User("id1", invalidUserName, validDisplayName, validPassword, validProjects));

            Assert.Throws<ArgumentException>(() =>
                new User("id1", validUserName, invalidDisplayName, validPassword, validProjects));

            Assert.Throws<ArgumentException>(() =>
                new User("id1", validUserName, validDisplayName, invalidPassword1, validProjects));

            Assert.Throws<ArgumentException>(() =>
                new User("id1", validUserName, validDisplayName, invalidPassword2, validProjects));

            Assert.Throws<ArgumentException>(() =>
                new User("id1", validUserName, validDisplayName, validPassword, invalidProjects));
        }

        [Fact]
        public void UserConstructorAndPropertyGettersWork()
        {
            var validUserName = "user1";
            var validDisplayName = "display1";
            var validPassword = UserValidationHelper.HashPassword("abc123");
            var validProjects = new UserProjects(new[]
            {
                new BasicProject("id2", "name1"),
                new BasicProject("id3", "name2"),
            });

            var user = new User("id1", validUserName, validDisplayName, validPassword, validProjects);

            Assert.Equal(validUserName, user.UserName);
            Assert.Equal(validDisplayName, user.DisplayName);
            Assert.Equal(validPassword, user.PasswordHash);
        }

        #region Helpers

        private static User CreateDefaultUser() => new User(
            "id",
            "username",
            "displayname",
            UserValidationHelper.HashPassword("passwordhash"),
            new UserProjects(new[]
            {
                new BasicProject("id1", "name1"),
                new BasicProject("id2", "name2")
            }));

        #endregion
    }
}
