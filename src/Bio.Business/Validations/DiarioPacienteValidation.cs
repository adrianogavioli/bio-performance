using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class DiarioPacienteValidation : AbstractValidator<DiarioPaciente>
    {
        public DiarioPacienteValidation()
        {
            RuleFor(a => a.Peso)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo deve ser maior que {ComparisonValue}");

            RuleFor(a => a.Observacao)
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        }
    }
}
