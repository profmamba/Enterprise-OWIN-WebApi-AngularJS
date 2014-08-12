using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Acutela.Frasset.Api.Logic;
using Acutela.Frasset.Api.Properties;
using Acutela.Frasset.Logic.Identity;
using Acutela.Frasset.Models.Api;
using Acutela.Frasset.Models.Api.Account;
using Acutela.Frasset.Objects.Identity;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Acutela.Frasset.Api.Controllers
{
	[RoutePrefix("Account")]
    public class AccountController : ApiController
    {
		//Fields

		private readonly ApiUserManager userManager;

		//Constructors

		public AccountController(IUserStore<User, Guid> store)
		{
			this.userManager = new ApiUserManager(store);
		}

		//Methods

		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//Create user
			var user = Mapper.Map<User>(model);

			var result = await userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			//Send account confirmation email & token
			string token = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
			var confirmUrl = string.Format("{0}?userId={1}&token={2}", Config.Url.GetAsFrontEndUrl(Config.Url.RegistrationConfirmation), user.Id, token);
			await userManager.SendEmailAsync(user.Id, Resources.ConfirmationSubject, string.Format(Resources.ConfirmationBody, user.Email, confirmUrl));

			return Ok(new ApiResponse { message = Resources.AccountRegisterOk });
		}

		[AllowAnonymous]
		[Route("Confirm")]
		public async Task<IHttpActionResult> Confirm(ConfirmationModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await userManager.ConfirmEmailAsync(model.UserId, model.ConfirmationToken);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
    }
}
