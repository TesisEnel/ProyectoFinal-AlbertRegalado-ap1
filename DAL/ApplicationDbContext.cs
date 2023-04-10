using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
{
    #nullable disable
    public DbSet<Cliente> Clientes {get; set;}
    public DbSet<Equipo> Equipos {get; set;}
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Suplidor> Suplidores {get; set;}
    public DbSet<Ventas> ventas { get; set; }
    public DbSet<VentasDetalle> ventasDetalles { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
      
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Pago>().HasData(  
        new Pago { PagoId = 1, Metodo = "Efectivo"});            
       

    }

}
