using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {   //Entity yapılandırmalarımızı belirleyeceğimiz yerdir.
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);//Id propum ForeignKeydir
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(7,2)");
            builder.Property(x => x.Stock).IsRequired();
            builder.HasOne(x => x.Category)//bir ürün bir kategoriye sahiptir
         .WithMany(x => x.Products)//bir kategoride bir den fazla ürün olabilir
         .HasForeignKey(x => x.CategoryId);
        }
    }
}
