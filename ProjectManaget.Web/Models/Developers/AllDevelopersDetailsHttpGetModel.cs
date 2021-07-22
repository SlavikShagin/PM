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

        public List<DeveloperEntity> DevelopersList { get; set; }
    }
}
