using OpenIddict.EntityFrameworkCore.Models;

namespace TraqCop.auth.Model.OpenIddict
{
     ///<inheritdoc/>
        public class TraqOpenIddictApplication : OpenIddictEntityFrameworkCoreApplication<Guid, TraqOpenIddictAuthorization, TraqOpenIddictToken>
        {
            /// <summary>
            /// 
            /// </summary>
            public TraqOpenIddictApplication()
            {
                Id = Guid.NewGuid();
            }
            public string AppId { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class TraqOpenIddictAuthorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, TraqOpenIddictApplication, TraqOpenIddictToken> { }
        /// <summary>
        /// 
        /// </summary>
        public class TraqOpenIddictScope : OpenIddictEntityFrameworkCoreScope<Guid> { }
        /// <summary>
        /// 
        /// </summary>
        public class TraqOpenIddictToken : OpenIddictEntityFrameworkCoreToken<Guid, TraqOpenIddictApplication, TraqOpenIddictAuthorization> { }
    

}
