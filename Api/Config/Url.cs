using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Acutela.Frasset.Api.Config
{
	public static class Url
	{
		//Properties

		public static string RegistrationConfirmation { get { return ConfigurationManager.AppSettings["Url.RegistrationConfirmation"]; } }
		public static string Api { get { return ConfigurationManager.AppSettings["Url.Api"]; } }
		public static string FrontEnd { get { return ConfigurationManager.AppSettings["Url.FrontEnd"]; } }

		//Static Methods

		public static string GetAsApiUrl(string url)
		{
			return string.Format("{0}{1}", Api, url);
		}

		public static string GetAsFrontEndUrl(string url)
		{
			return string.Format("{0}{1}", FrontEnd, url);
		}

	}
}