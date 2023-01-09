using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TraqCop.auth.Model.Maps
{
    public class AppUserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable(name: nameof(UserModel));
            SetupSuperAdmin(builder);
        }

        public PasswordHasher<UserModel> Hasher { get; set; } = new PasswordHasher<UserModel>();
        private void SetupSuperAdmin(EntityTypeBuilder<UserModel> builder)
        {
            var sysUser = new UserModel
            {
                Activated = true,
                CreatedOn = new DateTime(2023, 01, 08),
                FirstName = "John",
                LastName = "Doe",
                Id = Guid.Parse("426CC091-7876-4811-B472-35D5CB6BDD9A"),
                Email = "sholl45@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "sholl45@gmail.com".ToUpper(),
                PhoneNumber = "09067657843",
                UserName = "sholl45@gmail.com",
                NormalizedUserName = "sholl45@gmail.com".ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "micr0s0ft_"),
                SecurityStamp = "ED294048-52E4-4311-A7DA-4C1A862411F6",
                ModifiedBy = "sholl45@gmail.com",
                CreatedBy = "sholl45@gmail.com",
                IsPasswordDefault = false,
                IsDeleted = false,
            };

            var superUser = new UserModel
            {
                Activated = true,
                CreatedOn = new DateTime(2023, 01, 08),
                Id = Guid.Parse("508EACFC-E092-4B6C-828C-B93F9F77D582"),
                FirstName = "Prolifik",
                LastName = "Lexzy",
                Email = "leno78@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "leno78@gmail.com".ToUpper(),
                PhoneNumber = "07056543521",
                UserName = "leno78@gmail.com",
                NormalizedUserName = "leno78@gmail.com".ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "micr0s0ft_"),
                SecurityStamp = "7EAE80E0-137E-4318-8300-5C076F5AFC24",
                ModifiedBy = "leno78@gmail.com",
                CreatedBy = "leno78@gmail.com",
                IsPasswordDefault = false,
                IsDeleted = false,

            };
            builder.HasData(sysUser, superUser);
        }
    }
}
