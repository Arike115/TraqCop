using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TraqCop.auth.Helpers;

namespace TraqCop.auth.Model.Maps
{
    public class AppRoleMap : IEntityTypeConfiguration<AppRoles>
    {
        public void Configure(EntityTypeBuilder<AppRoles> builder)
        {
            builder.ToTable(name: nameof(AppRoles));
            SetupData(builder);
        }

        private void SetupData(EntityTypeBuilder<AppRoles> builder)
        {
            var roles = new AppRoles[]
            {
                new AppRoles
                {
                    Id = RoleHelpers.SYS_ADMIN_ID(),
                    Name = RoleHelpers.SYS_ADMIN,
                    NormalizedName = RoleHelpers.SYS_ADMIN.ToString()
                },

            };

            builder.HasData(roles);
        }
    }
}
