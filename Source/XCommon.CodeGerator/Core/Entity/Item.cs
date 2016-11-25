using System.Collections.Generic;
using System.Linq;

namespace XCommon.CodeGerator.Core.Entity
{
	public class Item
    {
		public string Name { get; set; }

		public string NameParent { get; set; }

		public string NameKey => Properties.FirstOrDefault(c => c.Key).Name;

		public List<ItemProperty> Properties { get; set; }

		public List<ItemRelationship> Relationships { get; set; }
	}
}
