using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TraqCop.auth.Model.Maps
{
    public class UserClaimMap : IEntityTypeConfiguration<UserClaims>
    {
        public void Configure(EntityTypeBuilder<UserClaims> builder)
        {
            builder.ToTable(nameof(UserClaims));
            builder.HasKey(c => c.Id);
        }
    }
}
