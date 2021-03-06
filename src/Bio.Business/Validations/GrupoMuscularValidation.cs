﻿using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class GrupoMuscularValidation : AbstractValidator<GrupoMuscular>
    {
        public GrupoMuscularValidation()
        {
            RuleFor(g => g.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
                .Length(3, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
