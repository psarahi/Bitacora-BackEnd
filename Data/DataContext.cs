using Bitacora.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitacora.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Actividad> Actividades { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}