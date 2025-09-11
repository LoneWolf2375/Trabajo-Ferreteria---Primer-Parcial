using Microsoft.EntityFrameworkCore;

namespace Administracion
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=productos.db");
        }
    }
}
