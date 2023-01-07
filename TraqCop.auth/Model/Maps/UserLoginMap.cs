using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TraqCop.auth.Model.Maps
{
    public class UserLoginMap : IEntityTypeConfiguration<UserLogins>
    {
        public void Configure(EntityTypeBuilder<UserLogins> builder)
        {
            builder.ToTable(nameof(UserLogins));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });
        }
    }
}
