using FluentValidation;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Repositories;
using System.Linq;

namespace Kzari.KitEscolar.Domain.Validators
{
    public class KitValidator : AbstractValidator<Kit>
    {
        private readonly IEntityBaseRepository<Kit> _repository;
        private readonly IEntityBaseRepository<Produto> _produtoRepository;

        public KitValidator(IEntityBaseRepository<Kit> repository, IEntityBaseRepository<Produto> produtoRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;

            RuleFor(kit => kit.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Necessário informar o nome do Kit.")

                .MinimumLength(3)
                .WithMessage("O Nome do Kit deve ter ao menos 3 caracteres.")

                .MaximumLength(50)
                .WithMessage("O Nome do Kit deve ter até 50 caracteres.")

                .Must((kit, nome) => KitUnico(kit))
                .WithMessage("Já existe um Kit com este nome cadastrado.");

            RuleFor(kit => kit.Itens)
                .Must((kit, itens) => !itens.Any() || itens.All(i => i.Quantidade > 0))
                .WithMessage("Necessário informar a quantidade de produtos de um ou mais itens.")

                .Must((kit, itens) => ProdutosSaoValidos(kit))
                .WithMessage("Um ou mais produtos são inválidos.");
        }

        private bool ProdutosSaoValidos(Kit kit)
        {
            if (!kit.Itens.Any())
                return true;

            int[] idProdutos = kit.Itens.Select(item => item.IdProduto).ToArray();

            int qtdeProdutosBase = _produtoRepository
                .SelecionarAsNoTracking()
                .Count(produto => produto.Ativo && idProdutos.Contains(produto.Id));

            return idProdutos.Count() == qtdeProdutosBase;
        }

        private bool KitUnico(Kit kit) => !_repository
            .SelecionarAsNoTracking()
            .Any(k => k.Nome.ToLower() == kit.Nome.ToLower() && 
                      (kit.Id == 0 || kit.Id != k.Id));
    }
}
