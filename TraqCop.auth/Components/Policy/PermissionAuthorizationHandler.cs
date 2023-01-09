using Microsoft.AspNetCore.Authorization;
using TraqCop.auth.Enums;

namespace TraqCop.auth.Components.Policy
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionsAuthorizationRequirement>
    {
        public PermissionAuthorizationHandler()
        {
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsAuthorizationRequirement requirement)
        {
            var currentUserPermissions = context.User.Claims
                .Where(x => x.Type.Equals(nameof(Permission))).Select(x => x.Value);

            var authorized = requirement.RequiredPermissions.AsParallel()
                .Any(rp => currentUserPermissions.Contains(rp.ToString()));

            if (authorized) context.Succeed(requirement);
        }
    }
}
