using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProGuessApplication.Models
{
    public class git
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nome_usuario { get; set; }
        [Required]
        public int usuario_id { get; set; }
        public string key { get; set; }
    }
}
