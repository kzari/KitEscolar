using Kzari.MateriaisEscolares.Infra.Data.DbContexts;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Kzari.MateriaisEscolares.Infra.Data.Repositories
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
