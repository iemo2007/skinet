using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.Id).HasName("PK_Product_Id");
        }
    }
}
