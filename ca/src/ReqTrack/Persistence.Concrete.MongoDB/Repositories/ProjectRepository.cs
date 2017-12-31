using System.Threading;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(MongoReqTrackDatabase database) : base(database)
        {
        }

        public Identity CreateProject(Project project)
        {
            var mongoProject = project.ToMongoEntity();

            var user = _userRepository.Read(mongoProject.AuthorId);
            if (user == null) { throw new EntityNotFoundException($"ID={project.Author.Id}"); }

            var projectWithSameNameOfAuthor = Builders<MongoProject>.Filter
                .Where(x => x.Name == mongoProject.Name && x.AuthorId == mongoProject.AuthorId);

            if (_projectRepository.Count(projectWithSameNameOfAuthor) != 0)
            {
                throw new AbandonedMutexException();
            }

            var id = _projectRepository.Create(mongoProject);

            _securityRightsRepository.Create(new MongoSecurityRights
            {
                CanChangeProjectRights = true,
                CanChangeRequirements = true,
                CanChangeUseCases = true,
                CanViewProject = true,
                IsAdministrator = true,
                ProjectId = id,
                UserId = user.Id,
            });

            return id.ToDomainIdentity();
        }

        public Project ReadProject(Identity id, bool loadRequirements, bool loadUseCases)
        {
            var project = _projectRepository.Read(id.ToMongoIdentity());
            if(project == null) { throw new EntityNotFoundException($"ID={id}"); }

            var user = _userRepository.Read(project.AuthorId);
            if(user == null) { throw new EntityNotFoundException($"ID={id}"); }

            var requirementFilter = Builders<MongoRequirement>.Filter.Eq(x => x.ProjectId, project.Id);
            var requirements = loadRequirements ? _requirementRepository.Find(requirementFilter) : null;

            var useCaseFilter = Builders<MongoUseCase>.Filter.Eq(x => x.ProjectId, project.Id);
            var useCases = loadUseCases ? _useCaseRepository.Find(useCaseFilter) : null;

            return project.ToDomainEntity(user, requirements, useCases);
        }

        public Project ReadProjectRequirements(Identity id, RequirementType type)
        {
            var project = _projectRepository.Read(id.ToMongoIdentity());
            if (project == null) { throw new EntityNotFoundException($"ID={id}"); }

            var user = _userRepository.Read(project.AuthorId);
            if (user == null) { throw new EntityNotFoundException($"ID={id}"); }

            var requirementsFilter = Builders<MongoRequirement>.Filter
                .Eq(x => x.ProjectId, project.Id);
            var typeFilter = Builders<MongoRequirement>.Filter
                .Eq(x => x.Type, type.ToString());

            var requirements = _requirementRepository.Find(requirementsFilter & typeFilter);

            return project.ToDomainEntity(user, requirements);
        }

        public bool UpdateProject(Project project, bool updateUseCases)
        {
            var mongoProject = project.ToMongoEntity();

            if (_projectRepository.Count(_projectRepository.IdFilter(mongoProject.Id)) == 0)
            {
                throw new EntityNotFoundException($"ID={project.Id}");
            }

            if (_userRepository.Count(_userRepository.IdFilter(mongoProject.AuthorId)) == 0)
            {
                throw new EntityNotFoundException($"ID={project.Author.Id}");
            }

            var projectWithSameNameOfAuthor = Builders<MongoProject>.Filter
                .Where(x => x.Name == project.Name && x.AuthorId == mongoProject.AuthorId);

            if (_projectRepository.Count(projectWithSameNameOfAuthor) != 0)
            {
                throw new AbandonedMutexException();
            }

            var filter = _projectRepository.IdFilter(mongoProject.Id);
            var updateDefinition = Builders<MongoProject>.Update
                .Set(x => x.Name, project.Name)
                .Set(x => x.Description, project.Description);
            var rv = _projectRepository.Update(filter, updateDefinition);

            if (rv && updateUseCases)
            {
                //domain collection ensures this iteration is ordered by order markers.
                var counter = 0;
                foreach (var r in project.Requirements)
                {
                    var updatefilter = _useCaseRepository.IdFilter(r.Id.ToMongoIdentity());
                    var refreshMarker = Builders<MongoUseCase>.Update.Set(x => x.OrderMarker, counter++);
                    //if this fells oh well, thanks mongo for not having transactions
                    _useCaseRepository.Update(updatefilter, refreshMarker);
                }
            }

            return rv;
        }

        public bool UpdateProjectRequirements(Project project, RequirementType type)
        {
            if (_projectRepository.Count(_projectRepository.IdFilter(project.Id.ToMongoIdentity())) == 0)
            {
                throw new EntityNotFoundException($"ID={project.Id}");
            }
            //TODO: modification date would be great

            var rv = true;
            var counter = 0;

            foreach (var r in project.Requirements)
            {
                var updatefilter = _requirementRepository.IdFilter(r.Id.ToMongoIdentity());
                var updateDefinition = Builders<MongoRequirement>.Update.Set(x => x.OrderMarker, counter++);
                if (!_requirementRepository.Update(updatefilter, updateDefinition))
                {
                    rv = false;
                    break;
                }
            }

            return rv;
        }

        public bool DeleteProject(Identity id)
        {
            var projectId = id.ToMongoIdentity();

            var filter = _projectRepository.IdFilter(projectId);
            if (_projectRepository.Count(filter) == 0) { throw new EntityNotFoundException($"ID={id}"); }

            if (!_projectRepository.Delete(filter)) { return false; }

            //remove the project from the users
            var securityRightsFilter = Builders<MongoSecurityRights>.Filter.Eq(x => x.ProjectId, projectId);
            _securityRightsRepository.Delete(securityRightsFilter);

            //remove requirements
            var requirementFilter = Builders<MongoRequirement>.Filter.Eq(x => x.ProjectId, projectId);
            _requirementRepository.Delete(requirementFilter);

            //remove use cases
            var useCaseFilter = Builders<MongoUseCase>.Filter.Eq(x => x.Id, projectId);
            _useCaseRepository.Delete(useCaseFilter);

            return true;
        }
    }
}
