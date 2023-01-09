using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TraqCop.auth.Utils.Guids;

namespace TraqCop.auth.Model
{
    public class UserModel : IdentityUser<Guid>, IEntity
    {
        public UserModel()
        {
            Id = SequentialGuidGenerator.Instance.Create();
            CreatedOn = DateTime.UtcNow;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Gender { get; set; }
        public bool Activated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPasswordDefault { get; set; }
       
    }

    public class UserClaims : IdentityUserClaim<Guid>
    {
    }

    public class UserLogins : IdentityUserLogin<Guid>
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }

    public class AppRoles : IdentityRole<Guid>
    {
        public AppRoles()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsInBuilt { get; set; } = false;
    }

    public class AppUserRoles : IdentityUserRole<Guid>
    {
    }

    public class AppRoleClaims : IdentityRoleClaim<Guid>
    {
    }

    public class AppUserTokens : IdentityUserToken<Guid>
    {
    }
}
