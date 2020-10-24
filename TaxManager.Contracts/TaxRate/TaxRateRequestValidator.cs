using FluentValidation;

namespace TaxManager.Contracts
{
    public class TaxRateRequestValidator : AbstractValidator<TaxRateRequest>
    {
        public TaxRateRequestValidator()
        {
            RuleFor(r => r.Municipality).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(r => r.Day).NotNull();
        }
    }
}