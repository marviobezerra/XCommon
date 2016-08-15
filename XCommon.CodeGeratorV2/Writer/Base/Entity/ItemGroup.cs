using System.Collections.Generic;

namespace XCommon.CodeGeratorV2.Writer.Base.Entity
{
	public class ItemGroup
    {
		public ItemGroup()
		{
			Items = new List<Item>();
		}

		public string Name { get; set; }
		
		public List<Item> Items { get; set; }
	}
}
