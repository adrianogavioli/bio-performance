using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class GrupoAlimentoValidation : AbstractValidator<GrupoAlimento>
    {
        public GrupoAlimentoValidation()
        {
            RuleFor(g => g.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(g => g.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
