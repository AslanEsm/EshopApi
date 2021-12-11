using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularEshop.Data.Confiq
{
    public class ProductSelectedCategoryConfiguration : IEntityTypeConfiguration<ProductSelectedCategory>
    {
        public void Configure(EntityTypeBuilder<ProductSelectedCategory> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(c => c.ProductSelectedCategories)
                   .IsRequired()
                   .HasForeignKey(p => p.ProductId);
            builder.HasOne(p => p.ProductCategory)
                   .WithMany(c => c.ProductSelectedCategories)
                   .IsRequired()
                   .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
