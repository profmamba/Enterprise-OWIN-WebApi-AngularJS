using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Models.Api.Account;
using Acutela.Frasset.Objects.Identity;
using AutoMapper;

namespace Acutela.Frasset.Models.Api
{
	public class ApiMappingConfig
	{
		public static void Register()
		{
			Mapper.CreateMap<RegisterModel, User>();
		}
	}
}
