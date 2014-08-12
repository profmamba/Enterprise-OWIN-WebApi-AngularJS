using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acutela.Frasset.Models.Api
{
	public class ApiRequest<T> where T:class
	{
		public T Value { get; set; }
	}
}
