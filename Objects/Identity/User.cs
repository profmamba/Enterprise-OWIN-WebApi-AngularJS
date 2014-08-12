using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Acutela.Frasset.Objects.Identity
{
	public class User : DomainObject, IUser<Guid>
	{
		//Properties

		public string UserName { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string SecurityStamp { get; set; }
		public virtual List<string> Roles { get; private set; }
		public virtual List<UserClaim> Claims { get; private set; }
		public virtual List<UserLoginInfo> Logins { get; private set; }

		//Constructors 

		public User()
		{
			this.Claims = new List<UserClaim>();
			this.Roles = new List<string>();
			this.Logins = new List<UserLoginInfo>();
		}

		public User(string userName)
			: this()
		{
			this.UserName = userName;
		}
	}
}
