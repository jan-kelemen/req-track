using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;

namespace ReqTrack.Domain.Core.Entities.Users
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : BasicUser
    {
        private string _userName;

        private string _passwordHash;

        private readonly IDictionary<Identity, BasicProject> _projectsById = new Dictionary<Identity, BasicProject>();
        private readonly IDictionary<string, BasicProject> _projectsByName = new SortedDictionary<string, BasicProject>();

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
            IEnumerable<BasicProject> projects) 
            : base(id, displayName)
        {
            UserName = userName;
            DisplayName = displayName;
            PasswordHash = passwordHash;
            AddProjects(projects);
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
                    throw new ArgumentException($"User name \"{value}\" is invalid.");
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
                    throw new ArgumentException($"Password hash \"{value}\" is invalid.");
                }

                _passwordHash = value;
            }
        }

        /// <summary>
        /// Projects of which the user is author in arbitrary order.
        /// </summary>
        public IEnumerable<BasicProject> Projects => _projectsByName.Values;

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
        public IEnumerable<Tuple<BasicProject, string>> CheckIfAllProjectsCanBeAdded(IEnumerable<BasicProject> projects)
        {
            var errorList = new List<Tuple<BasicProject, string>>();
            foreach (var project in projects)
            {
                if (HasProject(project.Name) && !HasProject(project.Id))
                {
                    errorList.Add(
                        new Tuple<BasicProject, string>(
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
        public void AddProjects(IEnumerable<BasicProject> projects)
        {
            var projectInfos = projects as BasicProject[] ?? projects.ToArray();

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
        public IEnumerable<Tuple<BasicProject, string>> CheckIfAllProjectsCanBeDeleted(IEnumerable<BasicProject> projects)
        {
            var errorList = new List<Tuple<BasicProject, string>>();
            foreach (var project in projects)
            {
                if (!HasProject(project.Id))
                {
                    errorList.Add(
                        new Tuple<BasicProject, string>(
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
        public void DeleteProjects(IEnumerable<BasicProject> projects)
        {
            var projectInfos = projects as BasicProject[] ?? projects.ToArray();

            var errors = CheckIfAllProjectsCanBeDeleted(projectInfos);
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
