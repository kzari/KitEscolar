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
        private readonly IMapper _mapper;
        private readonly IEntityBaseRepository<TEntity> _repository;

        protected AppServiceBaseValidation(IMapper mapper, IEntityBaseRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public TModel ObterPorId(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id não informado.");

            TEntity entidade = _repository.Obter(id);

            if (entidade == null)
                return null;

            return DeEntidadeParaViewModel(entidade);
        }

        public IEnumerable<TModel> Selecionar(bool somenteAtivos = true)
        {
            IEnumerable<TEntity> entidades = _repository
                .SelecionarAsNoTracking()
                .Where(a => !somenteAtivos || a.Ativo);

            return _mapper.Map<IEnumerable<TModel>>(entidades);
        }

        public virtual int Criar(TModel model)
        {
            TEntity entidade = DeViewModelParaEntidade(model);

            Validar(entidade);

            _repository.Inserir(entidade);

            return entidade.Id;
        }

        public virtual void Editar(int id, TModel model)
        {
            TEntity entidade = DeViewModelParaEntidade(model, id);

            entidade.DataAlteracao = DateTime.Now;

            Validar(entidade);

            _repository.Atualizar(entidade);
        }

        public void Deletar(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id não informado.");

            TEntity entidade = _repository.Obter(id);
            if (entidade == null)
                throw new ArgumentException("Não encontrado.");

            _repository.Excluir(entidade);
        }

        protected virtual TEntity DeViewModelParaEntidade(TModel model, int id = 0)
        {
            var entidade = _mapper.Map<TEntity>(model);
            entidade.Id = id;
            return entidade;
        }
        protected virtual TModel DeEntidadeParaViewModel(TEntity entidade) => _mapper.Map<TModel>(entidade);

        protected virtual IValidator<TEntity> ObterValidator()
        {
            Type validatorType = typeof(TValidator); 

            var construtorSemParametros = validatorType.GetConstructor(Type.EmptyTypes);
            if(construtorSemParametros != null)
                return Activator.CreateInstance<TValidator>();

            var construtorComRepositorio = validatorType.GetConstructor(new[] { _repository.GetType() });
            return (IValidator<TEntity>)construtorComRepositorio.Invoke(new[] { _repository });
        }
        protected virtual void Validar(TEntity entidade)
        {
            if (entidade == null)
                throw new Exception("Registros não detectados!");

            ObterValidator().ValidateAndThrow(entidade);
        }
    }
}
