using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TraqCop.auth.Helpers;

namespace TraqCop.auth.Model.Maps
{
    public class AppUserRoleMap : IEntityTypeConfiguration<AppUserRoles>
    {
        public void Configure(EntityTypeBuilder<AppUserRoles> builder)
        {
            builder.ToTable(name: nameof(AppUserRoles));
            builder.HasKey(p => new { p.UserId, p.RoleId });
            SetupData(builder);
        }

        private void SetupData(EntityTypeBuilder<AppUserRoles> builder)
        {
            List<AppUserRoles> dataList = new List<AppUserRoles>()
            {
                new AppUserRoles()
                {
                    UserId =Guid.Parse("426CC091-7876-4811-B472-35D5CB6BDD9A"),
                    RoleId = RoleHelpers.SYS_ADMIN_ID(),
                },
            };

            builder.HasData(dataList);
        }
    }
}
