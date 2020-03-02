using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kzari.MateriaisEscolares.Application.Models
{
    

    public class KitModel
    {
        [Required]
        public string Nome { get; set; }

        public List<ItemModel> Itens { get; set; }
    }

    public class ItemModel
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
