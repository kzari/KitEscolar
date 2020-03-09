using System.Collections.Generic;

namespace Kzari.KitEscolar.Domain.Entities
{
    public class Produto : Entidade
    {
        //ef
        public Produto() { }

        public Produto(string nome, string descricao = null)
        {
            Nome = nome;
            Descricao = descricao ?? string.Empty;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
