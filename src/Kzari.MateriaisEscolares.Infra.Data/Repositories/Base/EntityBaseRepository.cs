using Kzari.MateriaisEscolares.Infra.Data.DbContexts;
using Kzari.MaterialEscolar.Domain;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kzari.MateriaisEscolares.Infra.Data
{
    public class EntityBaseRepository<TEntity> : IEntityBaseRepository<TEntity> where TEntity : Entidade
    {
        private readonly MEContext _context;
        protected const int InserirEmLote_QtdeRegistrosInseridosPorVez = 20;

        public EntityBaseRepository(MEContext context)
        {
            _context = context;
        }


        public int Inserir(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();

            return obj.Id;
        }

        /// <summary>
        /// Insersão em Lote
        /// </summary>
        /// <param name="entidades"></param>
        public void InserirEmLote(IList<TEntity> entidades)
        {
            while (entidades.Any())
            {
                var entidadesSalvar = entidades.Take(InserirEmLote_QtdeRegistrosInseridosPorVez).ToList();
                _context.Set<TEntity>().AddRange(entidadesSalvar);
                _context.SaveChanges();

                entidades = entidades.Except(entidadesSalvar).ToList();
            }
        }

        public void Atualizar(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(c => c.DataCriacao).IsModified = false;
            _context.SaveChanges();
        }

        public void Excluir(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> SelecionarAsNoTracking(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = _context.Set<TEntity>().AsNoTracking();

            foreach (var includeExpression in includeExpressions)
                set = set.Include(includeExpression);

            return set;
        }

        public IEnumerable<TEntity> Selecionar()
        {
            return _context.Set<TEntity>();
        }

        public TEntity Obter(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
    }
}
