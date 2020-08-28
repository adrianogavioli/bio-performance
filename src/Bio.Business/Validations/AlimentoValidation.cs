using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class AlimentoValidation : AbstractValidator<Alimento>
    {
        public AlimentoValidation()
        {
            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(2, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Carboidratos)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(a => a.Proteinas)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(a => a.Gorduras)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(a => a.Calorias)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(a => a.Porcao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");
        }
    }
}
