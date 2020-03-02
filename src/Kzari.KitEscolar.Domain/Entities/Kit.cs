using System.Collections.Generic;

namespace Kzari.KitEscolar.Domain.Entities
{
    public class Kit : Entidade
    {
        public string Nome { get; set; }

        public virtual ICollection<Item> Itens { get; set; }
    }
}
