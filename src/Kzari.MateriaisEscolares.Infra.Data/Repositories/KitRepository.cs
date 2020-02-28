using Kzari.MateriaisEscolares.Infra.Data.DbContexts;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;

namespace Kzari.MateriaisEscolares.Infra.Data.Repositories
{
    public class KitRepository : EntityBaseRepository<Kit>, IKitRepository
    {
        public KitRepository(MEContext context) : base(context)
        {
        }
    }
}
