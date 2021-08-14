using System.Collections.Generic;

namespace ProjectManager.Database
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectSubject { get; set; }

#nullable enable
        public List<DeveloperEntity>? Developers { get; set; }
#nullable disable
    }
}
