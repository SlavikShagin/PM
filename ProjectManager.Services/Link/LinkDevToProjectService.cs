using ProjectManager.Database;
using ProjectManager.Repository;
using ProjectManager.Services.Link;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public class LinkDevToProjectService : ILinkDevToProjectService
    {
        public readonly IRepository<DeveloperEntity> _repositoryDev;
        public readonly IRepository<ProjectEntity> _repositoryPrj;

        public LinkDevToProjectService(IRepository<DeveloperEntity> repositoryDev,
            IRepository<ProjectEntity> repositoryPrj)
        {
            _repositoryDev = repositoryDev;
            _repositoryPrj = repositoryPrj;
        }

        public async Task Link(int developerId, int projectId)
        {
            var developer = _repositoryDev.GetById(developerId);
            var project = _repositoryPrj.GetById(projectId);

            var devlist = new List<DeveloperEntity>();
            devlist.Add(developer);

            project.Developers = devlist;

            _repositoryPrj.Update(project);
            _repositoryPrj.SaveChanges();
        }
        public async Task UnLink(int developerId, int projectId)
        {
            var project = _repositoryPrj.GetById(projectId);
            project.Developers.RemoveAt(developerId);
        }
    }
}
