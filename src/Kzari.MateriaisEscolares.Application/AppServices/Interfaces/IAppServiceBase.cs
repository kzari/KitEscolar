using Kzari.MaterialEscolar.Domain;
using System.Collections.Generic;

namespace Kzari.MateriaisEscolares.Application.AppServices.Base
{
    public interface IAppServiceBase<TEntity, TModel>
        where TEntity : Entidade
        where TModel : class
    {
        int Criar(TModel model);
        void Deletar(int id);
        void Editar(int id, TModel model);
        TModel ObterPorId(int id);
        IEnumerable<TModel> Selecionar(bool somenteAtivos = true);
    }
}