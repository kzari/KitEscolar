using FluentValidation;
using Kzari.MateriaisEscolares.Application.AppServices.Interfaces;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Kzari.MaterialEscolar.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kzari.MateriaisEscolares.Application.AppServices
{
    public class KitAppService : IKitAppService
    {
        private readonly IKitRepository _repository;


        public KitAppService(IKitRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<KitExibirModel> ObterTodos()
        {
            IEnumerable<KitExibirModel> kits = _repository
                .Selecionar()
                .Select(kit => new KitExibirModel { Id = kit.Id, Nome = kit.Nome })
                .ToList();

            return kits;
        }

        public KitExibirModel Obter(int id)
        {
            var kit = _repository.Obter(id);

            return kit == null ? 
                null : 
                new KitExibirModel { Id = kit.Id, Nome = kit.Nome };
        }

        public int Criar(KitModel model)
        {
            var entidade = new Kit(model.Nome);

            new KitValidator(_repository).ValidateAndThrow(entidade);

            return _repository.Inserir(entidade);
        }

        public void Editar(int id, KitModel model)
        {
            var entidade = _repository.Obter(id);

            if (entidade == null)
                throw new ArgumentException("Kit não encontrado.");

            entidade.Nome = model.Nome;
            entidade.DataAlteracao = DateTime.Now;

            new KitValidator(_repository).ValidateAndThrow(entidade);

            _repository.Atualizar(entidade);
        }
    }
}
