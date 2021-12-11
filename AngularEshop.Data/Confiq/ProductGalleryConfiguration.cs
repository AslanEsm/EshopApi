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
    public class ProductGalleryConfiguration : IEntityTypeConfiguration<ProductGallery>
    {
        public void Configure(EntityTypeBuilder<ProductGallery> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(c => c.ProductGalleries)
                   .IsRequired()
                   .HasForeignKey(p => p.ProductId);



        }
    }
}
