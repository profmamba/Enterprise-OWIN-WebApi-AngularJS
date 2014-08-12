using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Acutela.Frasset.Models.Api;

namespace Acutela.Frasset.Api.Controllers
{
	[RoutePrefix("Diagnostics")]
	public class DiagnosticsController : ApiController
	{
		[HttpGet, Route("Ping"), AllowAnonymous]
		public void Ping()
		{
			return;
		}

		[HttpPost, Route("Echo"), AllowAnonymous]
		public HttpResponseMessage Echo(ApiRequest<string> request)
		{
			return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse { message = request.Value });
		}

		[HttpGet, Route("AuthorisedPing")]
		public void AuthorisedPing()
		{
			return;
		}
	}
}
