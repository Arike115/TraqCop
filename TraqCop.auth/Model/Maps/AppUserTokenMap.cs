using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TraqCop.auth.Model.Maps
{
    public class AppUserTokenMap : IEntityTypeConfiguration<AppUserTokens>
    {
        public void Configure(EntityTypeBuilder<AppUserTokens> builder)
        {
            builder.ToTable(nameof(AppUserTokenMap));
            builder.HasKey(b => b.UserId);
        }
    }
}
