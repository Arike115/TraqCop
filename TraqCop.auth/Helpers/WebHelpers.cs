namespace TraqCop.auth.Helpers
{
    public class WebHelpers
    {
        private static IHttpContextAccessor _httpContextAccessor;

        //public static UserPrincipal CurrentUser
        //{
        //    get
        //    {
        //        //if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
        //        //{
        //        //    return new UserPrincipal(_httpContextAccessor.HttpContext.User);
        //        //}

        //        return new UserPrincipal(_httpContextAccessor.HttpContext.User);
        //    }
        //}

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get { return _httpContextAccessor.HttpContext; }
        }

        public static string GetRemoteIP
        {
            get { return HttpContext.Connection.RemoteIpAddress.ToString(); }
        }

        public static string GetUserAgent
        {
            get { return HttpContext.Request.Headers["User-Agent"].ToString(); }
        }

        public static string GetScheme
        {
            get { return HttpContext.Request.Scheme; }
        }
    }
}
