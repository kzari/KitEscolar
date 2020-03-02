using Kzari.KitEscolar.Infra.Data.DbContexts;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                .Include(a => a.Itens)
                .ThenInclude(a => a.Produto);
        }
    }
}
