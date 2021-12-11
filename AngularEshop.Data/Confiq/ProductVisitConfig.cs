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
    public class ProductVisitConfig : IEntityTypeConfiguration<ProductVisit>
    {
        public void Configure(EntityTypeBuilder<ProductVisit> builder)
        {
            builder.HasOne(p => p.Product)
                   .WithMany(c => c.ProductVisits)
                   .IsRequired()
                   .HasForeignKey(p => p.ProductId);
        }
    }
}
