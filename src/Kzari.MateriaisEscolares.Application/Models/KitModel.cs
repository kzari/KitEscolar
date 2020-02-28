using System.ComponentModel.DataAnnotations;

namespace Kzari.MateriaisEscolares.Application.Models
{
    public class KitModel
    {
        [Required]
        public string Nome { get; set; }
    }
}
