using FluentValidation;
using ProjectManager.Database;
using System.Collections.Generic;

namespace ProjectManager.Web.Models.Projects
{
    public class UpdateProjectHttpPutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectSubject { get; set; }
#nullable enable
        public ICollection<DeveloperEntity>? Developers { get; set; }
#nullable disable

        public class UpdateProjectHttpPutModelValidator : AbstractValidator<UpdateProjectHttpPutModel>
        {
            public UpdateProjectHttpPutModelValidator()
            {
                RuleFor(e => e.Name).NotEmpty();
                RuleFor(e => e.ProjectSubject).NotEmpty();
            }
        }
    }
}
