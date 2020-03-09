using Kzari.KitEscolar.Infra.Data.DbContexts;
using Kzari.KitEscolar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Kzari.KitEscolar.Domain.Repositories;

namespace Kzari.KitEscolar.Infra.Data.Repositories
{
    public class KitRepository : EntityBaseRepository<Kit>, IKitRepository
    {
        public KitRepository(MEContext context) : base(context)
        {
        }

        public override IEnumerable<Kit> SelecionarAsNoTracking()
        {
            return DbContext.Kits
                .AsNoTracking()
                .Include(a => a.Itens)
                .ThenInclude(a => a.Produto);
        }

        public override void Atualizar(Kit obj)
        {
            base.Atualizar(obj);

            //Atualizando itens
            var itens = DbContext.Itens.Where(i => i.IdKit == obj.Id);
            DbContext.Itens.RemoveRange(itens);
            DbContext.SaveChanges();

            DbContext.Itens.AddRange(obj.Itens);
            DbContext.SaveChanges();
        }
    }
}
