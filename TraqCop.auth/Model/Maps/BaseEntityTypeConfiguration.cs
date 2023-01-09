using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TraqCop.auth.Model.Maps
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasQueryFilter(m => m.IsDeleted == false);
            builder.Property(t => t.CreatedBy).HasMaxLength(150).IsUnicode(false);
            builder.Property(t => t.ModifiedBy).HasMaxLength(150).IsUnicode(false);
        }
    }
}
