using FluentValidation;

namespace ProjectManager.Web.Models.Developers
{
    public class UpdateDeveloperHttpPutModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateDeveloperHttpPutModelValidator : AbstractValidator<UpdateDeveloperHttpPutModel>
    {
        public UpdateDeveloperHttpPutModelValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty();
            RuleFor(e => e.LastName).NotEmpty();
            RuleFor(e => e.EMail).NotEmpty();
            RuleFor(e => e.EMail).EmailAddress();
            RuleFor(e => e.Phone).NotEmpty();
        }
    }
}
