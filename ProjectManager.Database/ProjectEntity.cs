using System.Collections.Generic;

namespace ProjectManager.Database
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectSubject { get; set; }

        public ICollection<DeveloperEntity> Developers { get; set; }
    }
}
