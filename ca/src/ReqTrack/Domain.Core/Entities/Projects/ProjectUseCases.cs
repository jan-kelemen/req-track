using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectUseCases : IEnumerable<ProjectUseCase>
    {
        private IDictionary<Identity, ProjectUseCase> _useCasesById;
        private IDictionary<string, ProjectUseCase> _useCasesByTitle;

        private List<ProjectUseCase> _useCases;

        public ProjectUseCases(IEnumerable<ProjectUseCase> useCases)
        {
            var dict = new Dictionary<Identity, ProjectUseCase>();
            var dictByTitle = new SortedDictionary<string, ProjectUseCase>();
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

            var list = new List<ProjectUseCase>(dict.Values);
            list.Sort();

            _useCasesById = dict;
            _useCasesByTitle = dictByTitle;
            _useCases = list;
        }

        public bool HasUseCase(Identity id) => _useCasesById.ContainsKey(id);

        public bool HasUseCase(string title) => _useCasesByTitle.ContainsKey(title);

        public IEnumerable<Tuple<ProjectUseCase, string>> CanAddUseCases(IEnumerable<ProjectUseCase> useCases)
        {
            return CanAddUseCases(useCases, _useCasesById, _useCasesByTitle);
        }

        public IEnumerable<Tuple<ProjectUseCase, string>> CanUpdateUseCases(IEnumerable<ProjectUseCase> useCases)
        {
            return CanUpdateUseCases(useCases, _useCasesById);
        }

        public IEnumerable<Tuple<ProjectUseCase, string>> CanDeleteUseCases(IEnumerable<ProjectUseCase> useCases)
        {
            return CanDeleteUseCases(useCases, _useCasesById);
        }

        public void ChangeUseCases(
            IEnumerable<ProjectUseCase> useCasesToAdd,
            IEnumerable<ProjectUseCase> useCasesToUpdate,
            IEnumerable<ProjectUseCase> useCasesToDelete)
        {
            var dict = new Dictionary<Identity, ProjectUseCase>(_useCasesById);
            var dictByTitle = new Dictionary<string, ProjectUseCase>(_useCasesByTitle);

            var toDelete = useCasesToDelete as ProjectUseCase[] ?? useCasesToDelete.ToArray();
            DeleteUseCases(toDelete, dict, dictByTitle);

            var toUpdate = useCasesToUpdate as ProjectUseCase[] ?? useCasesToUpdate.ToArray();
            UpdateUseCases(toUpdate, dict);

            var toAdd = useCasesToAdd as ProjectUseCase[] ?? useCasesToAdd.ToArray();
            AddUseCases(toAdd, dict, dictByTitle);

            _useCasesById = dict;
            _useCasesByTitle = dictByTitle;

            _useCases = new List<ProjectUseCase>(_useCasesById.Values);
            _useCases.Sort();
        }

        public IEnumerator<ProjectUseCase> GetEnumerator() => _useCases.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ProjectUseCase this[Identity id]
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

        public ProjectUseCase this[string title]
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

        private IEnumerable<Tuple<ProjectUseCase, string>> CanAddUseCases(
            IEnumerable<ProjectUseCase> useCases,
            IDictionary<Identity, ProjectUseCase> checkIn,
            IDictionary<string, ProjectUseCase> checkInByName)
        {
            var projectUseCases = useCases as ProjectUseCase[] ?? useCases.ToArray();

            return projectUseCases
                .Where(r => checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<ProjectUseCase, string>(r, "Use case already exists"))
                .Union(
                    projectUseCases
                        .Where(r => checkInByName.ContainsKey(r.Title))
                        .Select(r => new Tuple<ProjectUseCase, string>(r, "Use case with same title already exist"))
                    );
        }

        private IEnumerable<Tuple<ProjectUseCase, string>> CanUpdateUseCases(
            IEnumerable<ProjectUseCase> useCases
            , IDictionary<Identity, ProjectUseCase> checkIn)
        {
            return useCases
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<ProjectUseCase, string>(r, "Use case doesn't exist in project."));
        }

        private IEnumerable<Tuple<ProjectUseCase, string>> CanDeleteUseCases(
            IEnumerable<ProjectUseCase> useCases
            , IDictionary<Identity, ProjectUseCase> checkIn)
        {
            return useCases
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<ProjectUseCase, string>(r, "Use case doesn't exist in project."));
        }

        private void DeleteUseCases(
            ProjectUseCase[] toDelete, 
            IDictionary<Identity, ProjectUseCase> dict, 
            IDictionary<string, ProjectUseCase> dictByTitle)
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

        private void UpdateUseCases(ProjectUseCase[] toUpdate, IDictionary<Identity, ProjectUseCase> dict)
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
            ProjectUseCase[] toAdd,
            IDictionary<Identity, ProjectUseCase> dict,
            IDictionary<string, ProjectUseCase> dictByTitle)
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
