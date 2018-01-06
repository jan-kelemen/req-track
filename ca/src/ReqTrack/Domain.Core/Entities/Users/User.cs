using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;

namespace ReqTrack.Domain.Core.Entities.Users
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : BasicUser
    {
        private string _userName;

        private string _passwordHash;

        private readonly UserProjects _projects;

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="id">Identifier of the user, <see cref="Entity{T}.Id"/>.</param>
        /// <param name="userName">Username of the user, <see cref="UserName"/>.</param>
        /// <param name="displayName">Name of the user <see cref="BasicUser.DisplayName"/>.</param>
        /// <param name="passwordHash">Hashed passwordHash of the user <see cref="PasswordHash"/>.</param>
        /// <param name="projects">List of projects user can view.</param>
        /// <exception cref="ArgumentException">If some of passed properties were invalid.</exception>
        public User(
            Identity id,
            string userName,
            string displayName,
            string passwordHash,
            IEnumerable<BasicProject> projects = null)
            : base(id, displayName)
        {
            UserName = userName;
            DisplayName = displayName;
            PasswordHash = passwordHash;
            _projects = projects == null ? null : new UserProjects(projects);
        }

        /// <summary>
        /// Username of the user, can't be longer than <see cref="UserValidationHelper.MaximumUserNameLength"/> characters.
        /// </summary>
        /// <exception cref="ArgumentException">If passed user name is invalid. <see cref="UserValidationHelper.IsUserNameValid"/>.</exception>
        public string UserName
        {
            get => _userName;
            set
            {
                if (!UserValidationHelper.IsUserNameValid(value))
                {
                    throw new ValidationException("User name is invalid.")
                    {
                        PropertyKey = nameof(UserName),
                    };
                }

                _userName = value;
            }
        }

        /// <summary>
        /// Hash of the users current password, using the MD5 algorithm.
        /// </summary>
        /// <exception cref="ArgumentException">If passed password hash is invalid. <see cref="UserValidationHelper.IsPasswordHashValid"/></exception>
        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (!UserValidationHelper.IsPasswordHashValid(value))
                {
                    throw new ValidationException("Password hash is invalid.")
                    {
                        PropertyKey = "Password",
                    };
                }

                _passwordHash = value;
            }
        }

        public IEnumerable<BasicProject> Projects => _projects;

        public void ChangePassword(string newPassword, string oldPassword)
        {
            var oldHash = UserValidationHelper.HashPassword(oldPassword);
            if (oldHash != _passwordHash)
            {
                throw new ValidationException("Old passwod isn't correct.") { PropertyKey = "OldPassword" };
            }

            _passwordHash = UserValidationHelper.HashPassword(newPassword);
        }

        public bool CanAddProject(BasicProject project)
        {
            if (_projects == null)
            {
                throw new ApplicationException("");
            }

            return _projects.CanAddProject(project);
        }

        public bool CanDeleteProject(Identity id)
        {
            if (_projects == null)
            {
                throw new ApplicationException("");
            }

            return _projects.CanDeleteProject(id);
        }

        public void AddProject(BasicProject project)
        {
            if (_projects == null)
            {
                throw new ApplicationException("");
            }

            _projects.AddProject(project);
        }

        public void DeleteProject(Identity id)
        {
            if (_projects == null)
            {
                throw new ApplicationException("");
            }

            _projects.DeleteProject(id);
        }
    }
}
