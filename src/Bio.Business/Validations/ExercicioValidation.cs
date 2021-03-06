﻿using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class ExercicioValidation : AbstractValidator<Exercicio>
    {
        public ExercicioValidation()
        {
            RuleFor(a => a.Ordem)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}");

            RuleFor(a => a.Repeticao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Observacao)
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        }
    }
}
