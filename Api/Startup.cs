using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Acutela.Frasset.Models.Api;
using Microsoft.Owin.Security.OAuth;
using Acutela.Frasset.Api.Providers;
using Autofac;
using Acutela.Frasset.Api.Logic;
using Autofac.Integration.WebApi;
using Acutela.Frasset.Logic.Identity;
using Acutela.Frasset.Objects.Identity;
using Acutela.Frasset.Contexts;

[assembly: OwinStartup(typeof(Acutela.Frasset.Api.Startup))]
namespace Acutela.Frasset.Api
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();

			//Autofac dependency injection config
			var container = AutofacConfig.RegisterContainer(config);
			app.UseAutofacMiddleware(container);
			app.UseAutofacWebApi(config);

			//Authorization config
			var userManager = new ApiUserManager(new UserLogic<User>(new FrassetMongoContext()));
			ConfigureOAuth(app, userManager);

			//Automapper config
			ApiMappingConfig.Register();

			//WebApi configuration (routing etc.)
			WebApiConfig.Register(config);

			//Cors middleware
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			
			//Webapi middleware
			app.UseWebApi(config);
		}

		private void ConfigureOAuth(IAppBuilder app, ApiUserManager userManager)
		{
			OAuthAuthorizationServerOptions oauthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/Account/Token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new SimpleAuthorizationServerProvider(userManager)
			};

			app.UseOAuthAuthorizationServer(oauthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}


}