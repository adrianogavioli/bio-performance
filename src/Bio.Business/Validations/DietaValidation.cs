using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class DietaValidation : AbstractValidator<Dieta>
    {
        public DietaValidation()
        {
            RuleFor(d => d.TaxaMetabolicaBasal)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(d => d.Observacao)
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        }
    }
}
