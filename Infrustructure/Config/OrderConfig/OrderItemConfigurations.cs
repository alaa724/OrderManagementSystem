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
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderItem => orderItem.Product, product => product.WithOwner());

            builder.Property(OI => OI.UnitPrice)
                .HasColumnType("decimal(12,2)");

            builder.Property(OI => OI.Discount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.0);
        }
    }
}
