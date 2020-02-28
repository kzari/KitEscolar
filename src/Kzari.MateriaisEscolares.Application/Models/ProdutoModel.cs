using System.ComponentModel.DataAnnotations;

namespace Kzari.MateriaisEscolares.Application.Models
{
    public class ProdutoModel
    {
        [Required]
        public string Nome { get; set; }
    }
}
