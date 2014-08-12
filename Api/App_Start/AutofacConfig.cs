using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Acutela.Frasset.Api.Logic;
using Acutela.Frasset.Contexts;
using Acutela.Frasset.Interfaces.Contexts;
using Acutela.Frasset.Logic.Identity;
using Acutela.Frasset.Objects.Identity;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;

namespace Acutela.Frasset.Api
{
	public class AutofacConfig
	{
		public static IContainer RegisterContainer(HttpConfiguration config)
		{
			// Create the container builder.
			var builder = new ContainerBuilder();

			// Register the Web API controllers.
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			//Context
			builder.RegisterType<FrassetMongoContext>().As<IFrassetContext>().InstancePerRequest();
			
			//Logic
			builder.RegisterType<UserLogic<User>>().As<IUserStore<User, Guid>>().InstancePerRequest();
			
			//Web
			builder.RegisterType<ApiUserManager>().As<ApiUserManager>().InstancePerRequest();

			// Build the container.
			var container = builder.Build();

			// Create the dependency resolver.
			var resolver = new AutofacWebApiDependencyResolver(container);

			// Configure Web API with the dependency resolver.
			config.DependencyResolver = resolver;

			return container;
		}
	}
}