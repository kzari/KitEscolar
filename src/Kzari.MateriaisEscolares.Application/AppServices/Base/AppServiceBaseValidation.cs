using AutoMapper;
using FluentValidation;
using Kzari.MaterialEscolar.Domain;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kzari.MateriaisEscolares.Application.AppServices.Base
{
    /// <summary>
    /// Serviço de aplicação basico para CRUD com validação
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValidator"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public abstract class AppServiceBaseValidation<TEntity, TValidator, TModel> : IAppServiceBase<TEntity, TModel> 
        where TEntity : Entidade
        where TValidator : IValidator<TEntity>
        where TModel : class
    {
        protected readonly IMapper MapperInstance;
        protected readonly IEntityBaseRepository<TEntity> Repository;

        protected AppServiceBaseValidation(IMapper mapper, IEntityBaseRepository<TEntity> repository)
        {
            MapperInstance = mapper;
            Repository = repository;
        }

        public TModel ObterPorId(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id não informado.");

            TEntity entidade = Repository.Obter(id);

            if (entidade == null)
                return null;

            return DeEntidadeParaViewModel(entidade);
        }

        public virtual IEnumerable<TModel> Selecionar(bool somenteAtivos = true)
        {
            IEnumerable<TEntity> entidades = Repository
                .SelecionarAsNoTracking()
                .Where(a => !somenteAtivos || a.Ativo);

            return MapperInstance.Map<IEnumerable<TModel>>(entidades);
        }

        public virtual int Criar(TModel model)
        {
            TEntity entidade = DeViewModelParaEntidade(model);

            Validar(entidade);

            Repository.Inserir(entidade);

            return entidade.Id;
        }

        public virtual void Editar(int id, TModel model)
        {
            TEntity entidade = DeViewModelParaEntidade(model, id);

            entidade.DataAlteracao = DateTime.Now;

            Validar(entidade);

            Repository.Atualizar(entidade);
        }

        public void Deletar(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id não informado.");

            TEntity entidade = Repository.Obter(id);
            if (entidade == null)
                throw new ArgumentException("Não encontrado.");

            Repository.Excluir(entidade);
        }

        protected virtual TEntity DeViewModelParaEntidade(TModel model, int id = 0)
        {
            var entidade = MapperInstance.Map<TEntity>(model);
            entidade.Id = id;
            return entidade;
        }
        protected virtual TModel DeEntidadeParaViewModel(TEntity entidade) => MapperInstance.Map<TModel>(entidade);

        protected virtual IValidator<TEntity> ObterValidator()
        {
            Type validatorType = typeof(TValidator); 

            var construtorSemParametros = validatorType.GetConstructor(Type.EmptyTypes);
            if(construtorSemParametros != null)
                return Activator.CreateInstance<TValidator>();

            var construtorComRepositorio = validatorType.GetConstructor(new[] { Repository.GetType() });
            return (IValidator<TEntity>)construtorComRepositorio.Invoke(new[] { Repository });
        }
        protected virtual void Validar(TEntity entidade)
        {
            if (entidade == null)
                throw new Exception("Registros não detectados!");

            ObterValidator().ValidateAndThrow(entidade);
        }
    }
}
