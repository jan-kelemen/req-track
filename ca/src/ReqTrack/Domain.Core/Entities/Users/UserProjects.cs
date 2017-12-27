using System;
using System.Collections;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Projects;

namespace ReqTrack.Domain.Core.Entities.Users
{
    public class UserProjects : IEnumerable<BasicProject>
    {
        private readonly IDictionary<Identity, BasicProject> _projectsById;

        private readonly IDictionary<string, BasicProject> _projectsByName;

        public UserProjects(IEnumerable<BasicProject> projects)
        {
            var dict = new Dictionary<Identity, BasicProject>();
            var dictByName = new SortedDictionary<string, BasicProject>();
            foreach (var project in projects)
            {
                try
                {
                    dict.Add(project.Id, project);
                    dictByName.Add(project.Name, project);
                }
                catch
                {
                    throw new ArgumentException("Duplicate project found");
                }
            }

            _projectsById = dict;
            _projectsByName = dictByName;
        }

        public bool HasProject(Identity id) => _projectsById.ContainsKey(id);

        public bool HasProject(string name) => _projectsByName.ContainsKey(name);

        public bool CanAddProject(BasicProject project) => !HasProject(project.Id) && !HasProject(project.Name);

        public bool CanDeleteProject(Identity id) => !HasProject(id);

        public void AddProject(BasicProject project)
        {
            if (!CanAddProject(project))
            {
                throw new ArgumentException();
            }
            _projectsById.Add(project.Id, project);
            _projectsByName.Add(project.Name, project);
        }

        public void DeleteProject(Identity id)
        {
            if (!CanDeleteProject(id))
            {
                throw new ArgumentException();
            }

            var name = _projectsById[id].Name;
            _projectsById.Remove(id);
            _projectsByName.Remove(name);
        }

        public IEnumerator<BasicProject> GetEnumerator() => _projectsByName.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public BasicProject this[Identity id]
        {
            get
            {
                if (!HasProject(id))
                {
                    throw new ArgumentException();
                }

                return _projectsById[id];
            }
        }

        public BasicProject this[string name]
        {
            get
            {
                if (!HasProject((name)))
                {
                    throw new ArgumentException();
                }

                return _projectsByName[name];
            }
        }
    }
}
