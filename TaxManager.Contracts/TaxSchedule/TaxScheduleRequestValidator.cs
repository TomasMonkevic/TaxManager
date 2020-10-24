using FluentValidation;

namespace TaxManager.Contracts
{
    public class TaxScheduleRequestValidator : AbstractValidator<TaxScheduleRequest>
    {
        public TaxScheduleRequestValidator()
        {
            RuleFor(r => r.Municipality).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(r => r.TaxType).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(r => r.Rate).ExclusiveBetween(0.0, 1.0);
            RuleFor(r => r.From).NotNull();
        }
    }
}