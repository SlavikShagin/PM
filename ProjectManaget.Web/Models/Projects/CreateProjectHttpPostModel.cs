using FluentValidation;

namespace ProjectManager.Web.Models.Projects
{
    public class CreateProjectHttpPostModel
    {
        public string Name { get; set; }
        public string ProjectSubject { get; set; }
    }

    public class CreateProjectHttpPostModelValidator : AbstractValidator<CreateProjectHttpPostModel>
    {
        public CreateProjectHttpPostModelValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.ProjectSubject).NotEmpty();
        }
    }
}
