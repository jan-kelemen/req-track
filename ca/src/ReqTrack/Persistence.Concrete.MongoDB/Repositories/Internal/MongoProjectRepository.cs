using System.Threading;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal
{
    internal class MongoProjectRepository : MongoBaseRepository, IProjectRepository
    {
        private readonly IMongoCollection<MongoProject> _projects;

        private readonly IMongoCollection<MongoRequirement> _requirements;

        private readonly IMongoCollection<MongoUser> _users;

        private readonly IMongoCollection<MongoUseCase> _useCases;

        public MongoProjectRepository(
            IMongoCollection<MongoProject> projects,
            IMongoCollection<MongoRequirement> requirements,
            IMongoCollection<MongoUser> users,
            IMongoCollection<MongoUseCase> useCases)
        {
            _projects = projects;
            _requirements = requirements;
            _users = users;
            _useCases = useCases;
        }

        public Identity CreateProject(Project project)
        {
            var mongoProject = project.ToMongoEntity();

            var mongoUser = FindByIdOrThrow(_users, mongoProject.AuthorId);

            var projectWithSameNameOfAuthor = Builders<MongoProject>.Filter
                .Where(x => x.Name == mongoProject.Name && x.AuthorId == mongoProject.AuthorId);

            if (_projects.Count(projectWithSameNameOfAuthor) != 0)
            {
                throw new AbandonedMutexException();
            }

            _projects.InsertOne(mongoProject);

            var userFilter = Builders<MongoUser>.Filter.Eq(x => x.Id, mongoUser.Id);
            var userUpdateFilter = Builders<MongoUser>.Update.Push(x => x.AssociatedProjects, mongoProject.Id);
            _users.UpdateOne(userFilter, userUpdateFilter);

            return mongoProject.Id.ToDomainIdentity();
        }

        public Project ReadProject(Identity id, bool loadRequirements, bool loadUseCases)
        {
            var mongoProject = FindByIdOrThrow(_projects, id.ToMongoIdentity());
            var mongoUser = FindByIdOrThrow(_users, mongoProject.AuthorId);

            var projectRequirementsFilter = Builders<MongoRequirement>.Filter
                .Eq(x => x.ProjectId, mongoProject.Id);

            var requirements = loadRequirements ? _requirements.FindSync(projectRequirementsFilter) : null;

            //TODO: handle use cases

            return mongoProject.ToDomainEntity(mongoUser, requirements?.ToEnumerable());
        }

        public Project ReadProjectRequirements(Identity id, RequirementType type)
        {
            var mongoProject = FindByIdOrThrow(_projects, id.ToMongoIdentity());
            var mongoUser = FindByIdOrThrow(_users, mongoProject.AuthorId);

            var projectRequirementsFilter = Builders<MongoRequirement>.Filter
                .Eq(x => x.ProjectId, mongoProject.Id);
            var requirementTypeFilter = Builders<MongoRequirement>.Filter
                .Eq(x => x.Type, type.ToString());

            var requirements = _requirements.FindSync(projectRequirementsFilter & requirementTypeFilter);

            return mongoProject.ToDomainEntity(mongoUser, requirements.ToEnumerable());
        }

        public bool UpdateProject(Project project, bool updateUseCases)
        {
            var projectWithSameNameOfAuthor = Builders<MongoProject>.Filter
                .Where(x => x.Name == project.Name && x.AuthorId == project.Author.Id.ToMongoIdentity());

            if (_projects.Count(projectWithSameNameOfAuthor) != 0)
            {
                throw new AbandonedMutexException();
            }

            var filter = Builders<MongoProject>.Filter.Eq(x => x.Id, project.Id.ToMongoIdentity());
            var updateDefinition = Builders<MongoProject>.Update
                .Set(x => x.Name, project.Name)
                .Set(x => x.Description, project.Description);
            var options = new FindOneAndUpdateOptions<MongoProject>
            {
                ReturnDocument = ReturnDocument.After,
            };

            //TODO: handle use cases

            var mongoProject = _projects.FindOneAndUpdate(filter, updateDefinition, options);
            var mongoUser = FindByIdOrThrow(_users, mongoProject.AuthorId);

            return true;
        }

        public bool UpdateProjectRequirements(Project project, RequirementType type)
        {
            //just to check if the project still exists
            var mongoProject = FindByIdOrThrow(_projects, project.Id.ToMongoIdentity());

            //TODO: modification date would be great

            //domain collection ensures this iteration is ordered by order markers.
            var counter = 0;
            foreach (var r in project.Requirements)
            {
                var updatefilter = Builders<MongoRequirement>.Filter.Eq(x => x.Id, r.Id.ToMongoIdentity());
                var updateDefinition = Builders<MongoRequirement>.Update.Set(x => x.OrderMarker, counter++);

                _requirements.UpdateOne(updatefilter, updateDefinition);
            }

            return true;
        }

        public bool DeleteProject(Identity id)
        {
            var mongoProject = FindByIdOrThrow(_projects, id.ToMongoIdentity());

            //path of least damage if something fails

            //1. remove the project from the user
            var userFilter = Builders<MongoUser>.Filter.Eq(x => x.Id, mongoProject.AuthorId);
            var userUpdateDefinition = Builders<MongoUser>.Update.Pull(x => x.AssociatedProjects, mongoProject.Id);
            _users.UpdateOne(userFilter, userUpdateDefinition);

            //2. delete the project
            var projectFilter = Builders<MongoProject>.Filter.Eq(x => x.Id, mongoProject.Id);
            _projects.DeleteOne(projectFilter);

            //3. remove requirements
            var requirementFilter = Builders<MongoRequirement>.Filter.Eq(x => x.ProjectId, mongoProject.Id);
            _requirements.DeleteMany(requirementFilter);

            //4. remove use cases
            var useCaseFilter = Builders<MongoUseCase>.Filter.Eq(x => x.Id, mongoProject.Id);
            _useCases.DeleteMany(useCaseFilter);

            return true;
        }
    }
}
