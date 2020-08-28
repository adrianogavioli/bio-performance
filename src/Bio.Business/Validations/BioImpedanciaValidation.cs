using Bio.Business.Models;
using FluentValidation;

namespace Bio.Business.Validations
{
    public class BioImpedanciaValidation : AbstractValidator<BioImpedancia>
    {
        public BioImpedanciaValidation()
        {
            RuleFor(b => b.AguaCorporal)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(b => b.GorduraCorporal)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(b => b.Proteinas)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(b => b.Minerais)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");

            RuleFor(b => b.TaxaMetabolicaBasal)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido");
        }
    }
}
