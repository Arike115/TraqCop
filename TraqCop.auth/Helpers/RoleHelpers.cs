namespace TraqCop.auth.Helpers
{
    public static class RoleHelpers
    {
        public static Guid SYS_ADMIN_ID() => Guid.Parse("2D579044-E875-44CC-8E86-2ECBC29208C7");
        public const string SYS_ADMIN = nameof(SYS_ADMIN);

        public static List<string> GetAll()
        {
            return new List<string>
            {
                SYS_ADMIN,
               
            };
        }
    }
}
