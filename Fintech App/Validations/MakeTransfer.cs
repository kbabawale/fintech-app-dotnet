using System;
using Fintech_App.Model.DTO;
using FluentValidation;

namespace Fintech_App.Validations
{
    public class MakeTransferDTOValidator : AbstractValidator<MakeTransferDTO>
    {
        public MakeTransferDTOValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Amount)
                .NotEmpty().GreaterThan(0).WithMessage("Amount is required")
                .InclusiveBetween(100m, 1_000_000m).WithMessage("Amount must be between ₦100 and ₦1,000,000");

            RuleFor(x => x.SenderPin)
                .NotEmpty().WithMessage("Pin is required");

            RuleFor(x => x.SenderAccountNumber)
                .NotEmpty().WithMessage("Sender Account Number is required");

            RuleFor(x => x.ReceipientAccountNumber)
                .NotEmpty().WithMessage("Recipient Account Number is required");


        }
    }
}
