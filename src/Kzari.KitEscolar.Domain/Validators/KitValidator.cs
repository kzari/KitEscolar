using FluentValidation;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using System.Linq;

namespace Kzari.KitEscolar.Domain.Validators
{
    public class KitValidator : AbstractValidator<Kit>
    {
        private readonly IEntityBaseRepository<Kit> _repository;

        public KitValidator(IEntityBaseRepository<Kit> repository)
        {
            _repository = repository;

            RuleFor(kit => kit.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Necessário informar o nome do Kit.")

                .Must((kit, nome) => KitUnico(kit))
                .WithMessage("Já existe um Kit com este nome cadastrado.");
        }

        private bool KitUnico(Kit kit) => !_repository
            .Selecionar()
            .Any(k => k.Nome.ToLower() == kit.Nome.ToLower() && 
                      (kit.Id == 0 || kit.Id != k.Id));
    }
}
