using System.Collections.Generic;

namespace Kzari.MaterialEscolar.Domain.Entities
{
    public class Kit : Entidade
    {
        // ef
        private Kit() { }

        public Kit(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Kit(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
