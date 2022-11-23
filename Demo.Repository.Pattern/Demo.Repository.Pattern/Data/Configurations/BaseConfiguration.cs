using Demo.Repository.Pattern.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository.Pattern.Data.Configurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            var defaultBy = "System";

            // *.HasDefaultValueSql(Database.IsSqlite() ? "datetime('now', 'utc')" : "getutcdate()")

            builder.Property(p => p.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("now()");
            builder.Property(p => p.CreatedBy).ValueGeneratedOnAdd().HasDefaultValue(defaultBy);

            builder.Property(p => p.UpdatedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("now()");
            builder.Property(p => p.UpdatedBy).ValueGeneratedOnUpdate().HasDefaultValue(defaultBy);
        }
    }
}
