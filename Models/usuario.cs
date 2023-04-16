using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProGuessApplication.Models
{
    public class usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Nome do usuário")]
        public string nome { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Senha")]
        public string senha { get; set; }
        [Required]
        [Display(Name = "Perfil")]
        [DefaultValue("Padrão")]
        public string perfil { get; set; }
        [Required]
        [Display(Name = "Id do perfil")]
        [DefaultValue(1)]
        public int perfilId { get; set; }
        [DefaultValue(0)]
        public int deletado { get; set; }
        [DefaultValue(0)]
        public int desativado { get; set; }
        [DefaultValue("")]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }
    }
}
