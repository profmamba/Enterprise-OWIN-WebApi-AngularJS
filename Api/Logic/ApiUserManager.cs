using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Acutela.Frasset.Logic.Identity;
using System;
using Autofac.Core;
using Microsoft.Owin.Security.DataProtection;
using Acutela.Frasset.Objects.Identity;

namespace Acutela.Frasset.Api.Logic
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

	public class ApiUserManager : UserManager<User, Guid>
    {
		public ApiUserManager(IUserStore<User, Guid> store)
            : base(store)
        {
			// Configure validation logic for usernames
			UserValidator = new UserValidator<User, Guid>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			
			// Configure validation logic for passwords
			PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			var dataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Acutela.Frasset.Web");
			UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("Email Confirmation"));

			EmailService = new IdentityEmailService();
        }
    }
}
