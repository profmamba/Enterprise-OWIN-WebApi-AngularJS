using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acutela.Frasset.Interfaces.Repository;
using Acutela.Frasset.Objects.Identity;

namespace Acutela.Frasset.Interfaces.Contexts
{
	public interface IFrassetContext : IDisposable
	{
		/// <summary>
		/// System Users
		/// </summary>
		IRepository<User> Users { get; }
	}
}
