using Demo.Repository.Pattern.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Data.Configurations
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.Property(p => p.UnitPrice).HasDefaultValue(0.01d);
            base.Configure(builder);
        }
    }
}
