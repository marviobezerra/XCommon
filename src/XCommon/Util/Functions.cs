using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using XCommon.Application.Settings;
using XCommon.Extensions.String;
using XCommon.Extensions.Util;

namespace XCommon.Util
{
	public static class Functions
	{
		private static readonly Random getrandom = new Random();
		private static readonly object syncLock = new object();

		public static bool ValidEmail(string email)
		{
			if (email.IsEmpty())
			{
				return false;
			}

			return Regex.IsMatch(email, LibraryRegex.Email);
		}

		public static bool ValidUrl(string url)
		{
			if (url.IsEmpty())
			{
				return false;
			}

			return Uri.IsWellFormedUriString(url, UriKind.Absolute);
		}

		public static string GetToken(params int[] parts)
			=> GetToken('-', parts);

		public static string GetToken(char separator, params int[] parts)
		{
			var list = new List<string>();

			for (var i = 0; i < parts.Length; i++)
			{
				list.Add(GetRandomString(parts[i]));
			}

			return string.Join(separator.ToString(), list.ToArray());
		}

		public static string GetRandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length).Select(s => s[GetRandomNumber(0, s.Length)]).ToArray());
		}

		public static int GetRandomNumber(int min, int max)
		{
			lock (syncLock)
			{
				return getrandom.Next(min, max);
			}
		}

		public static IApplicationSettings GetApplicationSettings(string path, string section, string file = "appsettings.json")
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(path)
				.AddJsonFile(file, optional: true, reloadOnChange: true);

			var config = builder.Build();
			return config.Get<ApplicationSettings>(section);
		}
	}
}
