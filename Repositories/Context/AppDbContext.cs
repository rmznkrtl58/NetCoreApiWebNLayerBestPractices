using App.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories.Context
{
   public class AppDbContext(DbContextOptions<AppDbContext>options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Repositories Katmanım üzerindeki IEntityTypeConfiguration içeren sınıflarımı otomatik olarak tanımla (Assembly:repository katmanımı kapsar.)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
