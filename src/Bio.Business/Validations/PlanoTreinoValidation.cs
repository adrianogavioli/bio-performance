using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class PlanoTreinoValidation : AbstractValidator<PlanoTreino>
    {
        public PlanoTreinoValidation()
        {
            RuleFor(p => p.Observacao)
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        }
    }
}
