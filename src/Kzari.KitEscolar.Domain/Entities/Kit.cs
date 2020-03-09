using System.Collections.Generic;

namespace Kzari.KitEscolar.Domain.Entities
{
    public class Kit : Entidade
    {
        //ef
        private Kit()
        {
        }

        public Kit(string nome, ICollection<Item> itens = null)
        {
            Nome = nome;
            Itens = itens ?? new List<Item>();
        }

        public string Nome { get; set; }

        public virtual ICollection<Item> Itens { get; set; }
    }
}
