using Administracion;
using Microsoft.EntityFrameworkCore;

public class ProductoContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=productos.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}
