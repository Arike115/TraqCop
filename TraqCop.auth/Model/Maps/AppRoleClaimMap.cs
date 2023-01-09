using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security;
using TraqCop.auth.Helpers;
using TraqCop.auth.Enums;

namespace TraqCop.auth.Model.Maps
{
    public class AppRoleClaimMap : IEntityTypeConfiguration<AppRoleClaims>
    {
        private static int counter = 0;

        public void Configure(EntityTypeBuilder<AppRoleClaims> builder)
        {
            builder.ToTable(nameof(AppRoleClaims));
            SetupData(builder);
        }

        private void SetupData(EntityTypeBuilder<AppRoleClaims> builder)
        {
            var roleDictionary = new Dictionary<string, Guid>()
            {
                { RoleHelpers.SYS_ADMIN, RoleHelpers.SYS_ADMIN_ID()},
            };

            var permissions = (Permission[])Enum.GetValues(typeof(Permission));

            Array.ForEach(permissions, (p) =>
            {
                if (!string.IsNullOrWhiteSpace(p.GetPermissionCategory()) || roleDictionary.ContainsKey(p.GetPermissionCategory()))
                {
                    builder.HasData(new AppRoleClaims()
                    {
                        Id = ++counter,
                        RoleId = roleDictionary[p.GetPermissionCategory()],
                        ClaimType = nameof(Permission),
                        ClaimValue = p.ToString(),
                    });
                }
            });
        }

    }
}
