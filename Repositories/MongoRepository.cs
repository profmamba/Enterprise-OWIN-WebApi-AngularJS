using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Interfaces.Configuration;
using Acutela.Frasset.Interfaces.Repository;
using Acutela.Frasset.Objects;
using Acutela.Frasset.Repositories.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Acutela.Frasset.Repositories
{
	public class MongoRepository<T> : IRepository<T> where T : DomainObject
	{
		//Fields
		
		protected readonly MongoDatabase db;
		protected readonly MongoCollection collection;

		//Constructors

		public MongoRepository(MongoDatabase db)
		{
			this.collection = db.GetCollection<T>(typeof(T).Name);
		}

		//Methods

		protected virtual void RegisterMappings()
		{
			BsonClassMap.RegisterClassMap<T>(cm =>
			{
				cm.MapIdProperty(c => c.Id);

				cm.AutoMap();
				cm.IdMemberMap.SetIdGenerator(CombGuidGenerator.Instance);
			});
		}

		public void Dispose()
		{
			_provider.Dispose();
		}
	}
}
