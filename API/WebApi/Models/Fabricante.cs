using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Fabricante
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        [JsonIgnore]
        public ICollection<Cerveza> Cervezas { get; set; }
    }
}
