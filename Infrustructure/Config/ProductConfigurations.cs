using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(P => P.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(P => P.Stock)
                .IsRequired();
        }
    }
}
