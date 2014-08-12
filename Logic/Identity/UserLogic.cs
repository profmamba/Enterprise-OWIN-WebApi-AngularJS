using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Interfaces.Contexts;
using Acutela.Frasset.Interfaces.Logic.Identity;
using Acutela.Frasset.Objects.Identity;
using Microsoft.AspNet.Identity;

namespace Acutela.Frasset.Logic.Identity
{

	public class UserLogic<TUser> : IUserLogic, IUserLoginStore<TUser, Guid>, IUserClaimStore<TUser, Guid>,
		IUserPasswordStore<TUser, Guid>, IUserSecurityStampStore<TUser, Guid>, IUserEmailStore<TUser, Guid>, 
		IUserRoleStore<TUser, Guid>, IDisposable
		where TUser : User
	{
		//Fields

		private readonly IFrassetContext context;
		private bool disposed = false;

		//Constructors

		public UserLogic(IFrassetContext context)
		{
			this.context = context;
		}

		//IUserClaimStore Methods

		public Task AddClaimAsync(TUser user, Claim claim)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
			{
				user.Claims.Add(new UserClaim
				{
					ClaimType = claim.Type,
					ClaimValue = claim.Value
				});
			}

			return Task.FromResult(0);
		}

		public Task<IList<Claim>> GetClaimsAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			IList<Claim> result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
			return Task.FromResult(result);
		}

		public Task RemoveClaimAsync(TUser user, Claim claim)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

			return Task.FromResult(0);
		}

		//IUserLoginStore Methods

		public Task CreateAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.Run(() => context.Users.Add(user));
		}

		public Task DeleteAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.Run(() => context.Users.Delete(user));
		}

		public Task<TUser> FindByIdAsync(Guid userId)
		{
			ThrowIfDisposed();

			return Task.Run<TUser>(() => (TUser)context.Users.Find(userId));
		}

		public Task<TUser> FindByNameAsync(string userName)
		{
			ThrowIfDisposed();

			var user = context.Users.Where(u => u.UserName == userName).SingleOrDefault();

			return Task.FromResult((TUser)user);
		}

		public Task UpdateAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.Run(() => context.Users.Update(user));
		}

		public Task AddLoginAsync(TUser user, UserLoginInfo login)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			if (!user.Logins.Any(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey))
			{
				user.Logins.Add(login);
			}

			return Task.FromResult(true);
		}

		public Task<TUser> FindAsync(UserLoginInfo login)
		{
			ThrowIfDisposed();

			var query = context.Users
				.Where(u => u.Logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey));
			return Task.Run<TUser>(() => (TUser)query.SingleOrDefault());
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.Logins as IList<UserLoginInfo>);
		}

		public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
		{

			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.Logins.RemoveAll(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);

			return Task.FromResult(0);
		}

		//IUserPasswordStore Methods

		public Task<string> GetPasswordHashAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.PasswordHash != null);
		}

		public Task SetPasswordHashAsync(TUser user, string passwordHash)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.PasswordHash = passwordHash;

			return Task.FromResult(0);
		}

		//IUserSecurityStampStore Methods

		public Task<string> GetSecurityStampAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.SecurityStamp);
		}

		public Task SetSecurityStampAsync(TUser user, string stamp)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.SecurityStamp = stamp;

			return Task.FromResult(0);
		}

		//IUserEmailStore Methods

		public Task<TUser> FindByEmailAsync(string email)
		{
			ThrowIfDisposed();

			var query = context.Users
				.Where(u => u.Email == email);
	
			return Task.Run<TUser>(() => (TUser)query.SingleOrDefault());
		}

		public Task<string> GetEmailAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.EmailConfirmed);
		}

		public Task SetEmailAsync(TUser user, string email)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.Email = email;

			return Task.FromResult(0);
		}

		public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.EmailConfirmed = confirmed;

			return Task.FromResult(0);
		}

		//IUserRoleStore Methods

		public Task AddToRoleAsync(TUser user, string role)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			if (!user.Roles.Contains(role, StringComparer.InvariantCultureIgnoreCase))
				user.Roles.Add(role);

			return Task.FromResult(true);
		}

		public Task<IList<string>> GetRolesAsync(TUser user)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult<IList<string>>(user.Roles);
		}

		public Task<bool> IsInRoleAsync(TUser user, string role)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			return Task.FromResult(user.Roles.Contains(role, StringComparer.InvariantCultureIgnoreCase));
		}

		public Task RemoveFromRoleAsync(TUser user, string role)
		{
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException("user");

			user.Roles.RemoveAll(r => String.Equals(r, role, StringComparison.InvariantCultureIgnoreCase));

			return Task.FromResult(0);
		}

		//IDisposable Methods

		public void Dispose()
		{
			disposed = true;
		}

		//Methods

		private void ThrowIfDisposed()
		{
			if (disposed)
				throw new ObjectDisposedException(GetType().Name);
		}
	}
}
