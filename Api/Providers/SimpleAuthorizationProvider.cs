using Acutela.Frasset.Api.Logic;
using Acutela.Frasset.Api.Properties;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Acutela.Frasset.Api.Providers
{
	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		//Fields

		private readonly ApiUserManager userManager;
		
		//Constructors

		public SimpleAuthorizationServerProvider(ApiUserManager userManager)
		{
			this.userManager = userManager;
		}

		//Methods
		
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			return Task.Factory.StartNew<bool>(context.Validated);
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", Config.System.CorsDomains);

			var user = await userManager.FindAsync(context.UserName, context.Password);

			if (user == null)
			{
				context.SetError("invalid_grant", Resources.InvalidUserNameOrPassword);
				return;
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim("sub", context.UserName));

			//add roles that 
			var roles = await userManager.GetRolesAsync(user.Id);
			foreach (var role in roles)
				identity.AddClaim(new Claim("role", role));

			context.Validated(identity);
		}
	}
}