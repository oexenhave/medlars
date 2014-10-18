using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

namespace Medlars.Website
{
    using Microsoft.AspNet.Identity;

    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                        {
                                            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                            CookieName = "MedlarsAuthenticationCookie",
                                            LoginPath = new PathString("/Account/Authenticate"),
                                            CookieSecure = CookieSecureOption.SameAsRequest
                                        });
        }
    }
}
