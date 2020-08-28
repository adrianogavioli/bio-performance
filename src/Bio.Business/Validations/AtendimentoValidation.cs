using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class AtendimentoValidation : AbstractValidator<Atendimento>
    {
        public AtendimentoValidation()
        {
            RuleFor(a => a.Objetivo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 1000).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.RestricaoMedica)
                .Length(3, 1000).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.RestricaoAlimentar)
                .Length(3, 1000).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Observacao)
                .Length(3, 1000).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
