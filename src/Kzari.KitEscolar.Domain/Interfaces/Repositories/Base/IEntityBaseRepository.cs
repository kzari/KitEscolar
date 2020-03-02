using System.Collections.Generic;

namespace Kzari.KitEscolar.Domain.Interfaces.Repositories
{
    public interface IEntityBaseRepository<TEntity> where TEntity : Entidade
    {
        int Inserir(TEntity obj);
        void InserirEmLote(IList<TEntity> entidades);

        void Atualizar(TEntity obj);
        void Excluir(TEntity obj);

        TEntity Obter(int id);
        IEnumerable<TEntity> SelecionarAsNoTracking();
        IEnumerable<TEntity> Selecionar();
    }
}
