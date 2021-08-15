using Microsoft.EntityFrameworkCore;
using ProjectManager.Database;
using ProjectManager.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<ProjectEntity> _repository;

        public ProjectService(IRepository<ProjectEntity> repository)
        {
            _repository = repository;
        }

        public async Task Add(string name, string projectsubject)
        {
            ProjectEntity newRecord = new ProjectEntity()
            {
                Name = name,
                ProjectSubject = projectsubject,
            };
            _repository.Add(newRecord);
            _repository.SaveChanges();
        }

        public async Task<List<ProjectEntity>> GetAll()
        {
            List<ProjectEntity> projectsList;
            return projectsList = await _repository.Entities.Include(e=> e.Developers).OrderByDescending(e => e.Id).ToListAsync();
        }

        public async Task<ProjectEntity> GetById(int developerId)
        {
            var developer = await _repository.Entities.FirstOrDefaultAsync(e => e.Id == developerId);

            return developer;
        }

        public async Task<List<ProjectEntity>> GetByName(string name)
        {
            var projectsList = (
                from t in _repository.Entities
                where t.Name.Contains(name)
                select t)
                .ToList();
            return projectsList;
        }

        public async Task<ProjectEntity> DeleteProject(int projectId)
        {
            ProjectEntity entity = await _repository.Entities.FirstOrDefaultAsync(e => e.Id == projectId);
            _repository.Remove(entity);
            _repository.SaveChanges();
            return entity;
        }

        public async Task<ProjectEntity> UpdateProject(int id, string name, string projectsubject)
        {
            ProjectEntity updatedData = new ProjectEntity()
            {
                Id = id,
                Name = name,
                ProjectSubject = projectsubject,
            };

            _repository.Update(updatedData);
            _repository.SaveChanges();
            return updatedData;
        }
    }
}
