using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Objects;

namespace Acutela.Frasset.Interfaces.Repository
{
	public interface IRepository<T> where T : DomainObject
	{
		/// <summary>
		/// Add a new entity
		/// </summary>
		/// <param name="entity">The entity to add</param>
		/// <returns></returns>
		bool Add(T entity);

		/// <summary>
		/// Updates an entity
		/// </summary>
		/// <param name="entity">The updated entity</param>
		/// <returns></returns>
		bool Update(T entity);

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <param name="entity">The entity to delete</param>
		/// <returns></returns>
		bool Delete(T entity);
		
		/// <summary>
		/// Query the entries
		/// </summary>
		/// <param name="predicate">The match predicate</param>
		/// <returns></returns>
		IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
		
		/// <summary>
		/// Return a queryable interface for the repository
		/// </summary>
		/// <returns></returns>
		IQueryable<T> AsQueryable();
		
		/// <summary>
		/// Find a single entity by id
		/// </summary>
		/// <param name="id">The unique id of the entity to find</param>
		/// <returns></returns>
		T Find(Guid id);
	}
}
