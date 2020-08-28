using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class DietaRefeicaoAlimentoValidation : AbstractValidator<DietaRefeicaoAlimento>
    {
        public DietaRefeicaoAlimentoValidation()
        {
            RuleFor(d => d.Quantidade)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}");

            RuleFor(d => d.Observacao)
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        }
    }
}
