using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product.HasKey(t => t.Id);
            product.Property(p => p.Name).HasMaxLength(100).IsRequired();
            product.Property(p => p.Description).HasMaxLength(200).IsRequired();
            product.Property(p => p.Price).HasPrecision(10, 2);
            product.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
        }
    }
}
