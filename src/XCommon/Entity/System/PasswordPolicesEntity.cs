namespace XCommon.Entity.System
{
	public class PasswordPolicesEntity
	{
		public int MinLength { get; set; }

		public int MaxLength { get; set; }

		public bool RequireUppercaseChar { get; set; }

		public bool RequireLowercaseChar { get; set; }

		public bool RequireNumericChar { get; set; }

		public bool RequirePunctuationChar { get; set; }

		public bool AllowSpaces { get; set; }

		public bool AllowNonAscii { get; set; }

		public bool ShouldNotMatchUserId { get; set; }

		public int PasswordHistoryCount { get; set; }
	}
}
