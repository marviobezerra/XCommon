namespace XCommon.CodeGerator.Entity
{
	public class ItemRelationship
    {
		public string ItemGroupPK { get; set; }

		public string ItemPK { get; set; }

		public string PropertyPK { get; set; }

		public string ItemGroupFK { get; set; }

		public string ItemFK { get; set; }

		public string PropertyFK { get; set; }

		public bool Nullable { get; set; }

		public ItemRelationshipType RelationshipType { get; set; }
	}
}
