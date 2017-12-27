using System;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Users;
using Xunit;

namespace Domain.Core.Test.Unit.Entities.Users
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

            var validProjects = new[]
            {
                new User.ProjectInfo("id2", "name1"),
                new User.ProjectInfo("id3", "name2"),
            };
            var invalidProjects = new[]
            {
                new User.ProjectInfo("id4", "name3"),
                new User.ProjectInfo("id5", "name3"),
            };

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
            var validProjects = new[]
            {
                new User.ProjectInfo("id2", "name1"),
                new User.ProjectInfo("id3", "name2"),
            };

            var user = new User("id1", validUserName, validDisplayName, validPassword, validProjects);

            Assert.Equal(validUserName, user.UserName);
            Assert.Equal(validDisplayName, user.DisplayName);
            Assert.Equal(validPassword, user.PasswordHash);
        }

        [Fact]
        public void CantAddAProjectOfExistingNameAndDifferentIdToTheUser()
        {
            var user = CreateDefaultUser();
            var projectToAdd = new User.ProjectInfo("id3", "name1");
            var errors = user.CheckIfAllProjectsCanBeAdded(new[] {projectToAdd});
            var error = errors.FirstOrDefault();

            Assert.NotNull(error);
            Assert.Equal(projectToAdd.Id, error.Item1.Id);
            Assert.Equal(projectToAdd.Name, error.Item1.Name);

            Assert.Throws<ArgumentException>(() => user.AddProjects(new[] {projectToAdd}));
        }

        [Fact]
        public void CanAddProjectOfExistingIdUnderDifferentNameToTheUser()
        {
            var user = CreateDefaultUser();
            var projectToAdd = new User.ProjectInfo("id2", "name3");
            var errors = user.CheckIfAllProjectsCanBeAdded(new[] { projectToAdd });
            var error = errors.FirstOrDefault();

            Assert.Null(error);

            Assert.True(user.HasProject(projectToAdd.Id));
            Assert.False(user.HasProject(projectToAdd.Name));
            Assert.True(user.HasProject("name2"));

            user.AddProjects(new[] {projectToAdd});

            Assert.True(user.HasProject(projectToAdd.Id));
            Assert.True(user.HasProject(projectToAdd.Name));
            Assert.False(user.HasProject("name2"));
        }

        [Fact]
        public void CantDeleteAProjectFromUserIfHeIsntAnAuthor()
        {
            var user = CreateDefaultUser();
            var projectToDelete = new User.ProjectInfo("id3", "name2");
            var errors = user.CheckIfAllProjectsCanBeDeleted(new[] {projectToDelete});
            var error = errors.FirstOrDefault();

            Assert.NotNull(error);
            Assert.Equal(projectToDelete.Id, error.Item1.Id);
            Assert.Equal(projectToDelete.Name, error.Item1.Name);

            Assert.Throws<ArgumentException>(() => user.DeleteProjects(new[] { projectToDelete }));
        }

        [Fact]
        public void CanDeleteProjectEvenIfItsStoredUnderDifferentName()
        {
            var user = CreateDefaultUser();
            var projectToDelete = new User.ProjectInfo("id1", "name3");
            var errors = user.CheckIfAllProjectsCanBeDeleted(new[] { projectToDelete });
            var error = errors.FirstOrDefault();

            Assert.Null(error);

            Assert.True(user.HasProject(projectToDelete.Id));
            Assert.False(user.HasProject(projectToDelete.Name));
            Assert.True(user.HasProject("name2"));

            user.DeleteProjects(new[] { projectToDelete });

            Assert.False(user.HasProject(projectToDelete.Id));
            Assert.False(user.HasProject(projectToDelete.Name));
            Assert.True(user.HasProject("name2"));
        }

        [Fact]
        public void CorrectProjectIsDeletedFromTheUserEvenIfProjectOfTheSameNameExists()
        {
            var user = CreateDefaultUser();
            var projectToDelete = new User.ProjectInfo("id1", "name2");
            var errors = user.CheckIfAllProjectsCanBeDeleted(new[] { projectToDelete });
            var error = errors.FirstOrDefault();

            Assert.Null(error);

            Assert.True(user.HasProject(projectToDelete.Id));
            Assert.True(user.HasProject(projectToDelete.Name));
            Assert.True(user.HasProject("name1"));

            user.DeleteProjects(new[] { projectToDelete });

            Assert.False(user.HasProject(projectToDelete.Id));
            Assert.True(user.HasProject(projectToDelete.Name));
            Assert.False(user.HasProject("name1"));
        }

        #region Helpers

        private static User CreateDefaultUser() => new User(
            "id",
            "username",
            "displayname",
            UserValidationHelper.HashPassword("passwordhash"),
            new[]
            {
                new User.ProjectInfo("id1", "name1"),
                new User.ProjectInfo("id2", "name2")
            });

        #endregion
    }
}
