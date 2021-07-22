using ProjectManager.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Services.Project
{
    public interface IProjectService
    {
        Task<ProjectEntity> GetById(int developerId);
        Task<List<ProjectEntity>> GetAll();
        Task Add(string name, string projectsubject);
        Task<ProjectEntity> DeleteProject(int projectId);
        Task<ProjectEntity> UpdateProject(int id, string name, string projectsubject);
    }
}
