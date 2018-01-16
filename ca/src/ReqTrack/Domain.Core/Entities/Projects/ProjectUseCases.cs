using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectUseCases : IEnumerable<Project.UseCase>
    {
        private IDictionary<Identity, Project.UseCase> _useCasesById;
        private IDictionary<string, Project.UseCase> _useCasesByTitle;

        private List<Project.UseCase> _useCases;

        public ProjectUseCases(IEnumerable<Project.UseCase> useCases)
        {
            var dict = new Dictionary<Identity, Project.UseCase>();
            var dictByTitle = new SortedDictionary<string, Project.UseCase>();
            foreach (var useCase in useCases)
            {
                try
                {
                    dict.Add(useCase.Id, useCase);
                    dictByTitle.Add(useCase.Title, useCase);
                }
                catch
                {
                    throw new ArgumentException("Duplicate use case found");
                }
            }

            var list = new List<Project.UseCase>(dict.Values);
            list.Sort();

            _useCasesById = dict;
            _useCasesByTitle = dictByTitle;
            _useCases = list;
        }

        public bool HasUseCase(Identity id) => _useCasesById.ContainsKey(id);

        public bool HasUseCase(string title) => _useCasesByTitle.ContainsKey(title);

        public IEnumerable<Tuple<Project.UseCase, string>> CanAddUseCases(IEnumerable<Project.UseCase> useCases)
        {
            return CanAddUseCases(useCases, _useCasesById, _useCasesByTitle);
        }

        public IEnumerable<Tuple<Project.UseCase, string>> CanUpdateUseCases(IEnumerable<Project.UseCase> useCases)
        {
            return CanUpdateUseCases(useCases, _useCasesById);
        }

        public IEnumerable<Tuple<Project.UseCase, string>> CanDeleteUseCases(IEnumerable<Project.UseCase> useCases)
        {
            return CanDeleteUseCases(useCases, _useCasesById);
        }

        public void ChangeUseCases(
            IEnumerable<Project.UseCase> useCasesToAdd,
            IEnumerable<Project.UseCase> useCasesToUpdate,
            IEnumerable<Project.UseCase> useCasesToDelete)
        {
            var dict = new Dictionary<Identity, Project.UseCase>(_useCasesById);
            var dictByTitle = new Dictionary<string, Project.UseCase>(_useCasesByTitle);

            var toDelete = useCasesToDelete as Project.UseCase[] ?? useCasesToDelete.ToArray();
            DeleteUseCases(toDelete, dict, dictByTitle);

            var toUpdate = useCasesToUpdate as Project.UseCase[] ?? useCasesToUpdate.ToArray();
            UpdateUseCases(toUpdate, dict);

            var toAdd = useCasesToAdd as Project.UseCase[] ?? useCasesToAdd.ToArray();
            AddUseCases(toAdd, dict, dictByTitle);

            _useCasesById = dict;
            _useCasesByTitle = dictByTitle;

            _useCases = new List<Project.UseCase>(_useCasesById.Values);
            _useCases.Sort();
        }

        public IEnumerator<Project.UseCase> GetEnumerator() => _useCases.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Project.UseCase this[Identity id]
        {
            get
            {
                if (!HasUseCase(id))
                {
                    throw new ArgumentException("Use case doesn't exits");
                }

                return _useCasesById[id];
            }
        }

        public Project.UseCase this[string title]
        {
            get
            {
                if (!HasUseCase(title))
                {
                    throw new ArgumentException("Use case doesn't exits");
                }

                return _useCasesById[title];
            }
        }

        private IEnumerable<Tuple<Project.UseCase, string>> CanAddUseCases(
            IEnumerable<Project.UseCase> useCases,
            IDictionary<Identity, Project.UseCase> checkIn,
            IDictionary<string, Project.UseCase> checkInByName)
        {
            var projectUseCases = useCases as Project.UseCase[] ?? useCases.ToArray();

            return projectUseCases
                .Where(r => checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<Project.UseCase, string>(r, "Use case already exists"))
                .Union(
                    projectUseCases
                        .Where(r => checkInByName.ContainsKey(r.Title))
                        .Select(r => new Tuple<Project.UseCase, string>(r, "Use case with same title already exist"))
                    );
        }

        private IEnumerable<Tuple<Project.UseCase, string>> CanUpdateUseCases(
            IEnumerable<Project.UseCase> useCases
            , IDictionary<Identity, Project.UseCase> checkIn)
        {
            return useCases
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<Project.UseCase, string>(r, "Use case doesn't exist in project."));
        }

        private IEnumerable<Tuple<Project.UseCase, string>> CanDeleteUseCases(
            IEnumerable<Project.UseCase> useCases
            , IDictionary<Identity, Project.UseCase> checkIn)
        {
            return useCases
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<Project.UseCase, string>(r, "Use case doesn't exist in project."));
        }

        private void DeleteUseCases(
            Project.UseCase[] toDelete, 
            IDictionary<Identity, Project.UseCase> dict, 
            IDictionary<string, Project.UseCase> dictByTitle)
        {
            var deleteErrors = CanDeleteUseCases(toDelete);
            if (deleteErrors.Any())
            {
                throw new ArgumentException("Can't delete some of the use cases");
            }

            foreach (var useCase in toDelete)
            {
                var name = dict[useCase.Id].Title;
                dict.Remove(useCase.Id);
                dictByTitle.Remove(name);
            }
        }

        private void UpdateUseCases(Project.UseCase[] toUpdate, IDictionary<Identity, Project.UseCase> dict)
        {
            var updateErrors = CanUpdateUseCases(toUpdate, dict);
            if (updateErrors.Any())
            {
                throw new ArgumentException("Can't update some of the use cases");
            }

            foreach (var useCase in toUpdate)
            {
                dict[useCase.Id].OrderMarker = useCase.OrderMarker;
            }
        }

        private void AddUseCases(
            Project.UseCase[] toAdd,
            IDictionary<Identity, Project.UseCase> dict,
            IDictionary<string, Project.UseCase> dictByTitle)
        {
            var addErrors = CanAddUseCases(toAdd, dict, dictByTitle);
            if (addErrors.Any())
            {
                throw new ArgumentException("Can't add some of the use cases");
            }

            foreach (var useCase in toAdd)
            {
                dict.Add(useCase.Id, useCase);
                dictByTitle.Add(useCase.Title, useCase);
            }
        }
    }
}
