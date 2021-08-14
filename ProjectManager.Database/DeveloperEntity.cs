using System.Collections.Generic;

namespace ProjectManager.Database
{
    public class DeveloperEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }

#nullable enable
        public List<ProjectEntity>? Projects { get; set; }
#nullable disable
    }
}
