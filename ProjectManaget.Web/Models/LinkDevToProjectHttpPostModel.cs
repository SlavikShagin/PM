using FluentValidation;

namespace ProjectManager.Web.Models
{
    public class LinkDevToProjectHttpPostModel
    {
        public int developerId { get; set; }
        public int projectId { get; set; }

        public class LinkDevToProjectHttpPostModelValidator : AbstractValidator<LinkDevToProjectHttpPostModel>
        {
            public LinkDevToProjectHttpPostModelValidator()
            {
                RuleFor(e => e.developerId).NotEmpty();
                RuleFor(e => e.projectId).NotEmpty();
            }
        }
    }
}
