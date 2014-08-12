using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Acutela.Frasset.Logic.Config
{
	public static class Mail
	{
		public static string Server { get { return ConfigurationManager.AppSettings["Mail.Server"]; } }
		public static string User { get { return ConfigurationManager.AppSettings["Mail.User"]; } }
		public static string Password { get { return ConfigurationManager.AppSettings["Mail.Password"]; } }
		public static string AccountsFrom { get { return ConfigurationManager.AppSettings["Mail.From.Accounts"]; } }
		public static string AccountsFromName { get { return ConfigurationManager.AppSettings["Mail.From.Accounts.Name"]; } }
	}
}