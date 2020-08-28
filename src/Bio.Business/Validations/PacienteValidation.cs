using Bio.Business.Models;
using Bio.Business.Validations.Documentos;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class PacienteValidation : AbstractValidator<Paciente>
    {
        public PacienteValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.CPF.Length)
                .Equal(CPFValidation.TamanhoCpf).WithMessage("O campo CPF precisa ter {ComparisonValue} caracteres");

            RuleFor(p => CPFValidation.Validar(p.CPF))
                .Equal(true).WithMessage("O CPF informado é inválido");

            RuleFor(p => p.DataNascimento)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(p => p.Altura)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser mario que {ComparisonValue}");

            RuleFor(p => p.Genero)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");
        }
    }
}
