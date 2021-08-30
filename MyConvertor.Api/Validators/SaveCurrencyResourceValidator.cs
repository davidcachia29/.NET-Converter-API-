using FluentValidation;
using MyConvertor.Api.Resources;
using MyConvertor.Core.Models;

namespace MyConvertor.Api.Validations
{
    public class SaveDataResourceValidator : AbstractValidator<ConvertorDataGet>
    {
        public SaveDataResourceValidator()
        {
            RuleFor(m => m.fromCurrency)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.toCurrency)
                .NotEmpty();                

            RuleFor(m => m.Value)
               .NotEmpty()
               .WithMessage("'Amount Must Not Be 0");

            RuleFor(m => m.result)
               .NotEmpty();               
        }
    }
}