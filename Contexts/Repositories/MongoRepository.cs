using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Interfaces.Repository;
using Acutela.Frasset.Objects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Acutela.Frasset.Contexts.Repositories
{
	public class MongoRepository<T> : IRepository<T> where T : DomainObject
	{
		//Fields
		
		protected readonly MongoDatabase db;
		protected readonly MongoCollection<T> collection;

		//Constructors

		public MongoRepository(MongoDatabase db)
		{
			this.collection = db.GetCollection<T>(typeof(T).Name);
		}

		//Methods

		public bool Add(T entity)
		{
			return this.collection.Insert(entity).Ok;
		}

		public bool Update(T entity)
		{
			return collection.Save(entity)
				.DocumentsAffected > 0;
		}

		public bool Delete(T entity)
		{
			return collection.Remove(Query.EQ("_id", entity.Id))
				.DocumentsAffected > 0;
		}

		public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			return collection.AsQueryable().Where(predicate.Compile()).ToList();
		}

		public IQueryable<T> AsQueryable()
		{
			return collection.AsQueryable();
		}

		public T Find(Guid id)
		{
			return collection.FindOneById(id);
		}
	}
}
