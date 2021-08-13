using ProjectManager.Database;
using System.Collections.Generic;

namespace ProjectManager.Web.Models.Projects
{
    public class AllProjectsDetailsHttpGetModel
    {
        public AllProjectsDetailsHttpGetModel()
        {
            ProjectsList = new List<ProjectEntity>();
        }

        public PageViewModel PageViewModel { get; set; }
        public List<ProjectEntity> ProjectsList { get; set; }
    }
}
