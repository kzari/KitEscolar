using System.ComponentModel.DataAnnotations;

namespace Kzari.KitEscolar.Application.Models
{
    public class ProdutoModel
    {
        [Required]
        public string Nome { get; set; }
    }
}
