using System;
using System.Security.Cryptography;
using System.Text;

namespace XCommon.Util
{
	public static class Hash
	{
		public static string GetMD5(string input)
		{
			using (var md5 = MD5.Create())
			{
				return GetHash(md5, input);
			}
		}

		public static bool VerifyMD5(string input, string hash)
		{
			using (var md5 = MD5.Create())
			{
				return VerifyHash(md5, input, hash);
			}
		}

		public static string GetSHA1(string input)
		{
			using (var sha1 = SHA1.Create())
			{
				return GetHash(sha1, input);
			}
		}

		public static bool VerifySHA1(string input, string hash)
		{
			using (var sha1 = SHA1.Create())
			{
				return VerifyHash(sha1, input, hash);
			}
		}

		public static string GetSHA256(string input)
		{
			using (var sha256 = SHA256.Create())
			{
				return GetHash(sha256, input);
			}
		}

		public static bool VerifySHA256(string input, string hash)
		{
			using (var sha256 = SHA256.Create())
			{
				return VerifyHash(sha256, input, hash);
			}
		}

		private static string GetHash(HashAlgorithm hashAlgorithm, string input)
		{
			var data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sBuilder = new StringBuilder();

			for (var i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();
		}

		private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
		{
			var hashOfInput = GetHash(hashAlgorithm, input);
			return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash) == 0;
		}
	}
}
