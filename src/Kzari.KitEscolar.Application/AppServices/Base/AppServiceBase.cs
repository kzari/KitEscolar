using AutoMapper;
using FluentValidation;
using Kzari.KitEscolar.Domain;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kzari.KitEscolar.Application.AppServices.Base
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : Entidade
    {
        protected readonly IMapper AutoMapperInstance;
        protected readonly IEntityBaseRepository<TEntity> Repository;
        protected readonly IValidator<TEntity> Validator;
        
        protected AppServiceBase(IMapper mapper, IEntityBaseRepository<TEntity> repository, IValidator<TEntity> validator = null)
        {
            AutoMapperInstance = mapper;
            Repository = repository;
            Validator = validator;
        }

        public virtual int Criar<TModel>(TModel model) where TModel : class
        {
            TEntity entidade = DeViewModelParaEntidade(model);
            
            Validar(entidade);

            Repository.Inserir(entidade);

            return entidade.Id;
        }

        public virtual void Deletar(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id não informado.");

            TEntity entidade = Repository.Obter(id);
            if (entidade == null)
                throw new ArgumentException("Não encontrado.");

            Repository.Excluir(entidade);
        }

        public virtual void Editar<TModel>(int id, TModel model) where TModel : class
        {
            TEntity entidade = DeViewModelParaEntidade(model, id);

            entidade.DataAlteracao = DateTime.Now;

            Validar(entidade);

            Repository.Atualizar(entidade);
        }

        public virtual TModel ObterPorId<TModel>(int id) where TModel : class
        {
            if (id == 0)
                throw new ArgumentException("Id não informado.");

            TEntity entidade = Repository.Obter(id);

            if (entidade == null)
                return null;

            return AutoMapperInstance.Map<TModel>(entidade);
        }

        public virtual IEnumerable<TModel> Selecionar<TModel>(bool somenteAtivos) where TModel : class
        {
            IEnumerable<TEntity> entidades = Repository
                .SelecionarAsNoTracking()
                .Where(a => !somenteAtivos || a.Ativo);

            return AutoMapperInstance.Map<IEnumerable<TModel>>(entidades);
        }

        protected virtual TEntity DeViewModelParaEntidade<TModel>(TModel model, int id = 0)
        {
            TEntity entidade = AutoMapperInstance.Map<TEntity>(model);
            entidade.Id = id;
            return entidade;
        }

        protected virtual void Validar(TEntity entidade)
        {
            if (entidade == null)
                throw new Exception("Registros não detectados!");

            if (Validator != null)
                Validator.ValidateAndThrow(entidade);
        }
    }

    /// <summary>
    /// Serviço de aplicação basico para CRUD com validação (opcional)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValidator"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public class AppServiceBase<TEntity, TModel> : AppServiceBase<TEntity>, IAppServiceBase<TEntity, TModel>
        where TEntity : Entidade
        where TModel : class
    {
        public AppServiceBase(IMapper mapper, IEntityBaseRepository<TEntity> repository, IValidator<TEntity> validator = null)
            : base(mapper, repository, validator)
        {
        }

        public TModel ObterPorId(int id) => ObterPorId<TModel>(id);
        public virtual IEnumerable<TModel> Selecionar(bool somenteAtivos) => Selecionar<TModel>(somenteAtivos);
        public virtual int Criar(TModel model) => Criar<TModel>(model);
        public virtual void Editar(int id, TModel model) => Editar<TModel>(id, model);
        protected virtual TEntity DeViewModelParaEntidade(TModel model, int id = 0) => DeViewModelParaEntidade<TModel>(model, id);
    }

    /// <summary>
    /// Serviço de aplicação basico para CRUD com validação
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TValidator"></typeparam>
    public class AppServiceBase<TEntity, TModel, TValidator> : AppServiceBase<TEntity, TModel>, IAppServiceBase<TEntity, TModel, TValidator>
        where TEntity : Entidade
        where TModel : class
        where TValidator : IValidator<TEntity>
    {
        public AppServiceBase(IMapper mapper, IEntityBaseRepository<TEntity> repository, IValidator<TEntity> validator)
            : base(mapper, repository, validator)
        {
        }
    }
}
