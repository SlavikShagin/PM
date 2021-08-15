using System.Collections.Generic;

namespace ProjectManager.Database
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectSubject { get; set; }

#nullable enable
        public HashSet<DeveloperEntity>? Developers { get; set; }
#nullable disable
    }
}
