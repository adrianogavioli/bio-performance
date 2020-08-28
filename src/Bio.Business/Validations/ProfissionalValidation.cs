using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class ProfissionalValidation : AbstractValidator<Profissional>
    {
        public ProfissionalValidation()
        {

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.Documento)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");

            RuleFor(p => p.Especialidade)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 200).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        }
    }
}
