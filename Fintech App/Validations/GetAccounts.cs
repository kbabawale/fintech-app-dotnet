using System;
using Fintech_App.Model.DTO;
using FluentValidation;

namespace Fintech_App.Validations
{
    public class GetAccountDTOValidator : AbstractValidator<GetAccountsDTO>
    {
        public GetAccountDTOValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email Address is invalid")
                .NotEmpty().WithMessage("Email Address is required.");

        }
    }
}
