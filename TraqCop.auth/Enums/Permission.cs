using System.ComponentModel;
using TraqCop.auth.Helpers;

namespace TraqCop.auth.Enums
{
    public enum Permission
    {
        [Category(RoleHelpers.SYS_ADMIN), Description(@"Access All Modules")]
        FULL_CONTROL = 1001,
        
    }
}
