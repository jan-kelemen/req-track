using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReqTrack.Domain.Core.Entities.Users
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : Entity<User>
    {
        /// <summary>
        /// Helper class for common operations and constants related to the user.
        /// </summary>
        public static class Helper
        {        
            /// <summary>
            /// Maximum length of the user's userName.
            /// </summary>
            public static readonly int MaximumUserNameLength = 50;

            /// <summary>
            /// Maximum length of the user's display name.
            /// </summary>
            public static readonly int MaximumDisplayNameLenght = 100;

            /// <summary>
            /// Calculate the hash of the password.
            /// </summary>
            /// <param name="password">Password to hash.</param>
            /// <returns>Hashed password using the MD5 algorithm.</returns>
            public static string HashPassword(string password)
            {
                var hasher = System.Security.Cryptography.MD5.Create();
                var input = Encoding.UTF8.GetBytes(password);
                var output = hasher.ComputeHash(input);

                return output.Aggregate(string.Empty, (current, o) => current + o.ToString("x2"));
            }

            /// <summary>
            /// Checks if the passed user name represents a valid user name.
            /// </summary>
            /// <param name="userName">Username to be checked.</param>
            /// <returns><c>true</c> if the user name is valid.</returns>
            public static bool IsUserNameValid(string userName)
            {
                return !string.IsNullOrWhiteSpace(userName) && userName.Length <= MaximumUserNameLength;
            }

            /// <summary>
            /// Checks if the passed display name represents a valid display name.
            /// </summary>
            /// <param name="displayName">Display name to be checked.</param>
            /// <returns><c>true</c> if the display name is valid.</returns>
            public static bool IsDisplayNameValid(string displayName)
            {
                return !string.IsNullOrWhiteSpace(displayName) && displayName.Length <= MaximumDisplayNameLenght;
            }

            /// <summary>
            /// Checks if the password hash is valid.
            /// </summary>
            /// <param name="passwordHash"></param>
            /// <returns></returns>
            public static bool IsPasswordHashValid(string passwordHash)
            {
                return !string.IsNullOrWhiteSpace(passwordHash) && passwordHash.Length == 32;
            }
        }

        /// <summary>
        /// Basic information about the project of which the user is an author.
        /// </summary>
        public class ProjectInfo
        {
            public ProjectInfo(Identity id, string name)
            {
                Id = id;
                Name = name;
            }

            /// <summary>
            /// Identifier of the project.
            /// </summary>
            public Identity Id { get; }

            /// <summary>
            /// Name of the project.
            /// </summary>
            public string Name { get; }
        }

        private string _userName;

        private string _displayName;

        private string _passwordHash;

        private readonly IDictionary<Identity, ProjectInfo> _projectsById = new Dictionary<Identity, ProjectInfo>();
        private readonly IDictionary<string, ProjectInfo> _projectsByName = new SortedDictionary<string, ProjectInfo>();

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="id">Identifier of the user, <see cref="Entity{T}.Id"/>.</param>
        /// <param name="userName">Username of the user, <see cref="UserName"/>.</param>
        /// <param name="displayName">Name of the user <see cref="DisplayName"/>.</param>
        /// <param name="passwordHash">Hashed passwordHash of the user <see cref="PasswordHash"/>.</param>
        /// <param name="projects">Projects of the user, <see cref="CheckIfAllProjectsCanBeAdded"/>.</param>
        /// <exception cref="ArgumentException">If some of passed properties were invalid.</exception>
        public User(
            Identity id,
            string userName,
            string displayName,
            string passwordHash,
            IEnumerable<ProjectInfo> projects) 
            : base(id)
        {
            UserName = userName;
            DisplayName = displayName;
            PasswordHash = passwordHash;
            AddProjects(projects);
        }

        /// <summary>
        /// Username of the user, can't be longer than <see cref="Helper.MaximumUserNameLength"/> characters.
        /// </summary>
        /// <exception cref="ArgumentException">If passed user name is invalid. <see cref="Helper.IsUserNameValid"/>.</exception>
        public string UserName
        {
            get => _userName;
            set
            {
                if (!Helper.IsUserNameValid(value))
                {
                    throw new ArgumentException($"User name \"{value}\" is invalid.");
                }

                _userName = value;
            }
        }

        /// <summary>
        /// Display name of the user, can't be longer than <see cref="Helper.MaximumDisplayNameLenght"/> characters.
        /// </summary>
        /// <exception cref="ArgumentException">If passed display name is invalid. <see cref="Helper.IsDisplayNameValid"/></exception>
        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (!Helper.IsDisplayNameValid(value))
                {
                    throw new ArgumentException($"Display name \"{value}\" is invalid.");
                }

                _displayName = value;
            }
        }

        /// <summary>
        /// Hash of the users current password, using the MD5 algorithm.
        /// </summary>
        /// <exception cref="ArgumentException">If passed password hash is invalid. <see cref="Helper.IsPasswordHashValid"/></exception>
        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (!Helper.IsPasswordHashValid(value))
                {
                    throw new ArgumentException($"Password hash \"{value}\" is invalid.");
                }

                _passwordHash = value;
            }
        }

        /// <summary>
        /// Projects of which the user is author in arbitrary order.
        /// </summary>
        public IEnumerable<ProjectInfo> Projects => _projectsByName.Values;

        /// <summary>
        /// Checks if the user is an author on project with specified identity.
        /// </summary>
        /// <param name="id">Identity of the project.</param>
        /// <returns><c>true</c> if the user is an author of the project.</returns>
        public bool HasProject(Identity id)
        {
            return _projectsById.ContainsKey(id);
        }

        /// <summary>
        /// Checks if the user is an author on project with specified name.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <returns><c>true</c> if the user is an author of the project.</returns>
        public bool HasProject(string name)
        {
            return _projectsByName.ContainsKey(name);
        }

        /// <summary>
        /// Checks if all given projects can be added to the user.
        /// </summary>
        /// <param name="projects">Projects to add.</param>
        /// <returns>List of projects that can't be added.</returns>
        public IEnumerable<Tuple<ProjectInfo, string>> CheckIfAllProjectsCanBeAdded(IEnumerable<ProjectInfo> projects)
        {
            var errorList = new List<Tuple<ProjectInfo, string>>();
            foreach (var project in projects)
            {
                if (HasProject(project.Name) && !HasProject(project.Id))
                {
                    errorList.Add(
                        new Tuple<ProjectInfo, string>(
                            project,
                            "User is already an author on the project of the same name")
                    );
                }
            }

            return errorList;
        }

        /// <summary>
        /// Adds the given list of projects to the user.
        /// </summary>
        /// <param name="projects">Projects to add.</param>
        /// <returns><c>true</c> if operation was executed.</returns>
        /// <exception cref="ArgumentException">If users existing project contain a project of the same name but different id.</exception>
        public void AddProjects(IEnumerable<ProjectInfo> projects)
        {
            var projectInfos = projects.ToList();

            var errors = CheckIfAllProjectsCanBeAdded(projectInfos);
            if (errors.Any())
            {
                throw new ArgumentException("Projects with same name already exist for this user.");
            }

            foreach (var project in projectInfos)
            {
                //just update the name of the project
                if (HasProject(project.Id))
                {
                    var oldEntity = _projectsById[project.Id];

                    _projectsById[project.Id] = project;
                    _projectsByName.Remove(oldEntity.Name);
                    _projectsByName.Add(project.Name, project);
                }
                else
                {
                    _projectsById.Add(project.Id, project);
                    _projectsByName.Add(project.Name, project);
                }
            }
        }

        /// <summary>
        /// Checks if all given projects can be deleted from the user.
        /// </summary>
        /// <param name="projects">Projects to delete.</param>
        /// <returns>List of projects that can't be deleted.</returns>
        public IEnumerable<Tuple<ProjectInfo, string>> CheckIfAllProjectsCanBeDeleted(IEnumerable<ProjectInfo> projects)
        {
            var errorList = new List<Tuple<ProjectInfo, string>>();
            foreach (var project in projects)
            {
                if (!HasProject(project.Id))
                {
                    errorList.Add(
                        new Tuple<ProjectInfo, string>(
                            project,
                            "User isn't an author on one of the projects.")
                    );
                }
            }

            return errorList;
        }

        /// <summary>
        /// Removes the user as an author from the specified projects.
        /// </summary>
        /// <param name="projects">Projects to remove.</param>
        /// <exception cref="ArgumentException">If the user isn't an author on one of the passed projects.</exception>
        public void DeleteProjects(IEnumerable<ProjectInfo> projects)
        { 
            var projectInfos = projects.ToList();

            var errors = CheckIfAllProjectsCanBeAdded(projectInfos);
            if (errors.Any())
            {
                throw new ArgumentException("User isn't an author on one of the projects.");
            }

            foreach (var project in projectInfos)
            {
                //both maps contain same element as value, so even if the passed project list
                //contains projects with updated names, this will remove correct entities.
                _projectsByName.Remove(_projectsById[project.Id].Name);
                _projectsById.Remove(project.Id);
            }
        }
    }
}
