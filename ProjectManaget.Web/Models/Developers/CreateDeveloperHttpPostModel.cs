using FluentValidation;

namespace ProjectManager.Web.Models.Developers
{
    public class CreateDeveloperHttpPostModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
    }

    public class CreateDeveloperHttpPostModelValidator : AbstractValidator<CreateDeveloperHttpPostModel>
    {
        public CreateDeveloperHttpPostModelValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty();
            RuleFor(e => e.LastName).NotEmpty();
            RuleFor(e => e.EMail).NotEmpty();
            RuleFor(e => e.EMail).EmailAddress();
            RuleFor(e => e.Phone).NotEmpty();
        }
    }
}
