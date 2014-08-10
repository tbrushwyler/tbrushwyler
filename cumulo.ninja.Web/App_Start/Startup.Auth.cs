using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using cumulo.ninja.Web.Models;

namespace cumulo.ninja.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                //CookieName = "cumulo.ninja_" + ConfigurationManager.AppSettings["Environment"],
                //CookieDomain = "cumulo.ninja",
                //ExpireTimeSpan = System.TimeSpan.FromMinutes(60),
                SlidingExpiration = true,
                CookieSecure = CookieSecureOption.SameAsRequest
            });
            
            app.UseExternalSignInCookie();

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication(
               consumerKey: "VhcpSGoO4uSTfYpkU2RFcubrQ",
               consumerSecret: "UlcYRYPqN7UxZKXNj7StS0iJL9W0ObQffaj0Y0q3apnZXWLnoI");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}