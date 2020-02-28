using FluentValidation;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using System.Linq;

namespace Kzari.MaterialEscolar.Domain.Validators
{
    public class KitValidator : AbstractValidator<Kit>
    {
        private readonly IKitRepository _repository;

        public KitValidator(IKitRepository repository)
        {
            _repository = repository;

            RuleFor(kit => kit.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Necessário informar o Nome.")

                .Must((kit, nome) => KitUnico(kit))
                .WithMessage("Já existe um Kit com este nome cadastrado.");
        }

        private bool KitUnico(Kit kit) => !_repository
            .Selecionar()
            .Any(k => k.Nome.ToLower() == kit.Nome.ToLower() && 
                      (kit.Id == 0 || kit.Id != k.Id));
    }
}
