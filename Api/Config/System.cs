using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Acutela.Frasset.Api.Config
{
	public static class System
	{
		private static string[] corsDomains = null;

		public static string[] CorsDomains
		{
			get
			{
				if (corsDomains == null)
					corsDomains = ConfigurationManager.AppSettings["System.CorsDomains"].Split(',');
				return corsDomains;
			}
		}
	}
}