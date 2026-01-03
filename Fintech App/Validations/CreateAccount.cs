using Fintech_App.Model.DTO;
using FluentValidation;

namespace Fintech_App.Validations
{


    public class CreateAccountDTOValidator : AbstractValidator<CreateAccountDTO>
    {
        public CreateAccountDTOValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email Address is invalid")
                .NotEmpty().WithMessage("Email Address is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");
        }
    }
}
