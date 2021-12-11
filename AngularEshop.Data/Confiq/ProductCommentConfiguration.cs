using AngularEshop.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularEshop.Data.Confiq
{
    public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(c => c.ProductComments)
                .IsRequired()
                .HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.User)
                .WithMany(c => c.ProductComments)
                .IsRequired()
                .HasForeignKey(d => d.UserId);
        }
    }
}