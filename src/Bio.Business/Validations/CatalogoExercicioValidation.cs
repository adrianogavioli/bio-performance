using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class CatalogoExercicioValidation : AbstractValidator<CatalogoExercicio>
    {
        public CatalogoExercicioValidation()
        {
            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 300).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
