
namespace WebApi.Models
{
    public class Cerveza
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        
        public Fabricante Fabricante { get; set; }
        public int FabricanteId { get; set; }

        public decimal Precio { get; set; }

    }
}
