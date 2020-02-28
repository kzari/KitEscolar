using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kzari.MaterialEscolar.Domain.Interfaces.Repositories
{
    public interface IEntityBaseRepository<TEntity> where TEntity : Entidade
    {
        int Inserir(TEntity obj);
        void InserirEmLote(IList<TEntity> entidades);

        void Atualizar(TEntity obj);
        void Excluir(TEntity obj);

        TEntity Obter(int id);
        IEnumerable<TEntity> SelecionarAsNoTracking(params Expression<Func<TEntity, object>>[] includeExpressions);
        IEnumerable<TEntity> Selecionar();
    }
}
