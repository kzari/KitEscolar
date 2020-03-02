using System.Collections.Generic;

namespace Kzari.MaterialEscolar.Domain.Entities
{
    public class Kit : Entidade
    {
        public string Nome { get; set; }

        public virtual ICollection<Item> Itens { get; set; }
    }
}
