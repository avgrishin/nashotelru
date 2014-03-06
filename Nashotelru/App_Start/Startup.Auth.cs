using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nashotelru
{
  public partial class Startup
  {
    // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureAuth(IAppBuilder app)
    {
      CookieAuthenticationProvider provider = new CookieAuthenticationProvider();
      var originalHandler = provider.OnApplyRedirect;
      provider.OnApplyRedirect = context =>
      {

        var mvcContext = new HttpContextWrapper(HttpContext.Current);
        var routeData = RouteTable.Routes.GetRouteData(mvcContext);

        //Get the current language  
        RouteValueDictionary routeValues = new RouteValueDictionary();
        routeValues.Add("culture", routeData.Values["culture"]);

        //Reuse the RetrunUrl
        Uri uri = new Uri(context.RedirectUri);
        string returnUrl = HttpUtility.ParseQueryString(uri.Query)[context.Options.ReturnUrlParameter];
        routeValues.Add(context.Options.ReturnUrlParameter, returnUrl);

        routeValues.Add("area", "");
        
        //Overwrite the redirection uri
        var u = new UrlHelper(HttpContext.Current.Request.RequestContext);
        context.RedirectUri = u.Action("Login", "Account", routeValues);

        originalHandler.Invoke(context);
      };

      // Enable the application to use a cookie to store information for the signed in user
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login"),
        Provider = provider
      });
      // Use a cookie to temporarily store information about a user logging in with a third party login provider
      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      // Uncomment the following lines to enable logging in with third party login providers
      app.UseMicrosoftAccountAuthentication(
          clientId: "000000004C10FF0D",
          clientSecret: "JBRs2Rpqw8vJUP30a68hCAlKZkTYcAgs");

      //app.UseTwitterAuthentication(
      //   consumerKey: "",
      //   consumerSecret: "");

      app.UseFacebookAuthentication(
         appId: "282540021895058",
         appSecret: "303aca413053259133fa6d75f3dc4b84");

      app.UseGoogleAuthentication();
    }
  }
}