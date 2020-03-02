using Kzari.MateriaisEscolares.Infra.Data.DbContexts;
using Kzari.MaterialEscolar.Domain;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kzari.MateriaisEscolares.Infra.Data
{
    public class EntityBaseRepository<TEntity> : IEntityBaseRepository<TEntity> where TEntity : Entidade
    {
        protected readonly MEContext DbContext;
        protected const int InserirEmLote_QtdeRegistrosInseridosPorVez = 20;

        public EntityBaseRepository(MEContext context)
        {
            DbContext = context;
        }


        public virtual int Inserir(TEntity obj)
        {
            DbContext.Set<TEntity>().Add(obj);
            DbContext.SaveChanges();

            return obj.Id;
        }

        /// <summary>
        /// Insersão em Lote
        /// </summary>
        /// <param name="entidades"></param>
        public virtual void InserirEmLote(IList<TEntity> entidades)
        {
            while (entidades.Any())
            {
                var entidadesSalvar = entidades.Take(InserirEmLote_QtdeRegistrosInseridosPorVez).ToList();
                DbContext.Set<TEntity>().AddRange(entidadesSalvar);
                DbContext.SaveChanges();

                entidades = entidades.Except(entidadesSalvar).ToList();
            }
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbContext.Entry(obj).State = EntityState.Modified;
            DbContext.Entry(obj).Property(c => c.DataCriacao).IsModified = false;
            DbContext.SaveChanges();
        }

        public virtual void Excluir(TEntity obj)
        {
            DbContext.Set<TEntity>().Remove(obj);
            DbContext.SaveChanges();
        }

        public virtual IEnumerable<TEntity> SelecionarAsNoTracking()
        {
            IQueryable<TEntity> set = DbContext.Set<TEntity>().AsNoTracking();

            return set;
        }

        public virtual IEnumerable<TEntity> Selecionar()
        {
            return DbContext.Set<TEntity>();
        }

        public virtual TEntity Obter(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }
    }
}
