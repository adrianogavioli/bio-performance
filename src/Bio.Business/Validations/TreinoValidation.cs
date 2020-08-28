using Bio.Business.Models;
using FluentValidation;
using System.Linq;

namespace Bio.Business.Validations
{
    public class TreinoValidation : AbstractValidator<Treino>
    {
        public TreinoValidation()
        {
            RuleFor(a => a.Ordem)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}");

            RuleFor(a => a.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Observacao)
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");

            RuleFor(x => x.GruposMusculares)
                .Must(list => list.Count() > 0).WithMessage("Selecione ao menos um grupo muscular");
        }
    }
}
