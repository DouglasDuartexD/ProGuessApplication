using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ProGuessApplication.Models
{
    public partial class Root
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("node_id")]
        public string node_id { get; set; }
        [JsonProperty("name")]
        [Display(Name = "Nome do repositório")]
        public string name { get; set; }
        [JsonProperty("owner")]
        [Display(Name = "Nome do criador")]
        public Owner owner { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }
        [JsonProperty("published_at")]
        public DateTime published_at { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataCriacao { get; set; }
        [DataType(DataType.Time)]
        public DateTime? HoraCriacao { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataUltimoUpdate { get; set; }
        [DataType(DataType.Time)]
        public DateTime? HoraUltimoUpdate { get; set; }
        [Display(Name = "Id Interno")]
        public int repositorio_id { get; set; }
    }
    public partial class Owner
    {
        [JsonProperty("login")]
        [Display(Name = "Dono")]
        public string name { get; set; }
        [JsonProperty("avatar_url")]
        public string avatar_url { get; set; }
    }
}
