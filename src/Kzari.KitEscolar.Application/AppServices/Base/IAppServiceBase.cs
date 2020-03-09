using FluentValidation;
using Kzari.KitEscolar.Domain;
using System.Collections.Generic;

namespace Kzari.KitEscolar.Application.AppServices.Base
{
    public interface IAppServiceBase<TEntity> where TEntity : Entidade
    {
        int Criar<TModel>(TModel model) where TModel : class;
        void Deletar(int id);
        void Editar<TModel>(int id, TModel model) where TModel : class;
        TModel ObterPorId<TModel>(int id) where TModel : class;
        IEnumerable<TModel> Selecionar<TModel>(bool somenteAtivos) where TModel : class;
    }

    public interface IAppServiceBase<TEntity, TModel> : IAppServiceBase<TEntity>
        where TEntity : Entidade
        where TModel : class
    {
        int Criar(TModel model);
        void Editar(int id, TModel model);
        TModel ObterPorId(int id);
        IEnumerable<TModel> Selecionar(bool somenteAtivos);
    }

    public interface IAppServiceBase<TEntity, TModel, TValidator> : IAppServiceBase<TEntity, TModel>
        where TEntity : Entidade
        where TModel : class
        where TValidator : IValidator<TEntity>
    {
    }
}