using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class RefeicaoValidation : AbstractValidator<Refeicao>
    {
        public RefeicaoValidation()
        {
            RuleFor(r => r.Ordem)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}");

            RuleFor(r => r.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
