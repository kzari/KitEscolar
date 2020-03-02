using System.Collections.Generic;

namespace Kzari.KitEscolar.Application.Models
{
    public class KitExibirModel
    {
        public string Nome { get; set; }
        public List<ItemExibirModel> Itens { get; set; }
    }

    public class ItemExibirModel
    {
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
    }
}
