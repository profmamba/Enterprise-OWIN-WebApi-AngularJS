using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Acutela.Frasset.Objects
{
	public abstract class DomainObject
	{
		public virtual Guid Id { get; set; }
	}
}
