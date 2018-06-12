namespace XCommon.Application.Settings
{
	public class ApplicationAuthentication
	{
		/// <summary>
		/// Token issuer
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// Token audience
		/// </summary>
		public string Audience { get; set; }

		/// <summary>
		/// Token expiration in days
		/// </summary>
		public int Expiration { get; set; }

		/// <summary>
		/// Token Security Key
		/// </summary>
		public string SecurityKey { get; set; }
	}
}
