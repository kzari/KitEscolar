﻿using FluentValidation;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using System.Linq;

namespace Kzari.MaterialEscolar.Domain.Validators
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
                .Must((produto, nome) => ProdutoUnico(produto))
                .WithMessage($"Já existe um Produto com este nome cadastrado.");
        }

        public bool ProdutoUnico(Produto produto) => !_repository
            .Selecionar()
            .Any(p => p.Nome.ToLower() == produto.Nome.ToLower() && 
                      (p.Id == 0 || p.Id != produto.Id));
    }
}