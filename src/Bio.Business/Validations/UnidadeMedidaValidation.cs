using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class UnidadeMedidaValidation : AbstractValidator<UnidadeMedida>
    {
        public UnidadeMedidaValidation()
        {

            RuleFor(u => u.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(1, 10).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(u => u.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
