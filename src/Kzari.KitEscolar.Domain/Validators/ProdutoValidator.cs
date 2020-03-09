using FluentValidation;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using System.Linq;

namespace Kzari.KitEscolar.Domain.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        private readonly IEntityBaseRepository<Produto> _repository;

        public ProdutoValidator(IEntityBaseRepository<Produto> repository)
        {
            _repository = repository;

            RuleFor(produto => produto.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("O Produto deve ter um nome.")

                .MinimumLength(3)
                .WithMessage("O Nome do Kit deve ter ao menos 3 caracteres.")

                .MaximumLength(50)
                .WithMessage("O Nome do Kit deve ter até 50 caracteres.")

                .Must((produto, nome) => ProdutoUnico(produto))
                .WithMessage($"Já existe um Produto com este nome cadastrado.");
        }

        public bool ProdutoUnico(Produto produto) => !_repository
            .SelecionarAsNoTracking()
            .Any(p => p.Nome.ToLower() == produto.Nome.ToLower() && 
                      (p.Id == 0 || p.Id != produto.Id));
    }
}
