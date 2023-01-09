namespace TraqCop.auth.Configs
{
    public class AuthSettings
    {
        public string PrivateKey { get; set; }
        public string SecretKey { get; set; }
        public string Authority { get; set; }
        public bool RequireHttps { get; set; }
        public int TokenExpiry { get; set; }
        public string Issuer { get; set; }
        public string Password { get; set; }
        public string Redirect_Url { get; set; }
    }
}
