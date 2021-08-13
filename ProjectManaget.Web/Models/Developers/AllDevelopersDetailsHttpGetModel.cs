using ProjectManager.Database;
using System.Collections.Generic;

namespace ProjectManager.Web.Models.Developers
{
    public class AllDevelopersDetailsHttpGetModel
    {
        public AllDevelopersDetailsHttpGetModel()
        {
            DevelopersList = new List<DeveloperEntity>();
        }

        public PageViewModel PageViewModel { get; set; }
        public List<DeveloperEntity> DevelopersList { get; set; }
    }
}
