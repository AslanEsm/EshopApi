using AngularEshop.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularEshop.Data.Confiq
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(c => c.OrderDetails)
                .IsRequired()
                .HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.Order)
                .WithMany(c => c.OrderDetails)
                .IsRequired()
                .HasForeignKey(p => p.OrderId);
        }
    }
}