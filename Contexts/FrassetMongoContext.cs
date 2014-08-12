using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Contexts.Repositories;
using Acutela.Frasset.Interfaces.Contexts;
using Acutela.Frasset.Interfaces.Repository;
using Acutela.Frasset.Objects.Exceptions;
using Acutela.Frasset.Objects.Identity;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Acutela.Frasset.Contexts
{
	public class FrassetMongoContext : IFrassetContext
	{
		//Fields

		protected readonly MongoDatabase db;

		//Properties

		public IRepository<User> Users { get; private set; }

		//Constructors

		public FrassetMongoContext()
		{
			//set all Id guids to use COMB generator
			BsonSerializer.RegisterIdGenerator(typeof(Guid), CombGuidGenerator.Instance);

			//set db connection
			var url = MongoUrl.Create(ConfigurationManager.ConnectionStrings[Constants.FrassetDbConnectionKey].ConnectionString);

			if (url.DatabaseName == null)
				throw new Frasset.Objects.Exceptions.ConfigurationException(@"Database name was not specified in connection string, e.g. http:\\localhost:27017\Frasset");

			var client = new MongoClient(url);
			var server = client.GetServer();

			this.db = server.GetDatabase(url.DatabaseName);

			InitialiseRepositories();
		}

		//Methods

		protected virtual void InitialiseRepositories()
		{
			this.Users = new MongoRepository<User>(db);
		}

		public void Dispose()
		{
			//nothing to do - mongo driver manages own connection pools
		}
	}
}
