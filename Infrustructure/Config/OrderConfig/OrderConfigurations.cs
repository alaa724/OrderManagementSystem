using Core.Entities.Order_Agregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Config.OrderConfig
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.Status)
                .HasConversion(
                    (OStatus) => OStatus.ToString(),
                    (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                 );

            builder.Property(order => order.TotalAmount)
                .HasColumnType("decimal(12,2)");

            builder.Property(order => order.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(order => order.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
