using System.Collections.Generic;

namespace Kzari.MaterialEscolar.Domain.Entities
{
    public class Produto : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
