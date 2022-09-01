using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Context
{
    public class DespachoContext : DbContext
    {
        public DbSet<Fabricante> Fabricantes { get; set; }
         
        public DbSet<Cerveza> Cervezas { get; set; }


        public DespachoContext(DbContextOptions<DespachoContext> options) : base(options)
        {
            
        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Fabricante> fabricantesList = new List<Fabricante>();
            List<Cerveza> cervezasList = new List<Cerveza>();
            fabricantesList.Add(
            new Fabricante()
            {
                Id = 1,
                Nombre = "Quilmes"
            });
            fabricantesList.Add(new Fabricante()
            {
                Id = 2,
                Nombre = "Patagonia"

            });


            //adding some data
            cervezasList.Add(
             new Cerveza{
                 Id = 1,
                 Nombre = "Classic",
                 Descripcion = "Cerveza más elegida desde 1890.",
                 FabricanteId = 1
            });

            cervezasList.Add(
                new Cerveza
                {
                    Id = 2,
                    Nombre = "IPA",
                    Descripcion = "Cerveza Ipa fabricanda en el sur de la Patagonia Argentina.",
                    FabricanteId = 2
                });

            modelBuilder.Entity<Fabricante>(f =>
            {
                //define the name of the table
                f.ToTable("Fabricante");

                //define the pk
                f.HasKey(f => f.Id);

                //define properties of the table
                f.Property(f => f.Nombre).IsRequired().HasMaxLength(100);

                f.HasData(fabricantesList);
            });

             modelBuilder.Entity<Cerveza>(c =>
            {
                //define the name of the table
                c.ToTable("Cerveza");
                
                //define the pk
                c.HasKey(c => c.Id);
                
                //define the fk
                c.HasOne(c => c.Fabricante).WithMany(f => f.Cervezas).HasForeignKey(c => c.FabricanteId);
                
                //define the properties of the table
                c.Property(c=> c.Nombre).IsRequired().HasMaxLength(100);
                c.Property(c=> c.Descripcion).IsRequired().HasMaxLength(300);
                c.HasData(cervezasList);


            });
        }
    }
}
