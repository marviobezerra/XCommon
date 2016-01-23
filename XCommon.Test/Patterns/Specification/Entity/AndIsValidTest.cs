using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
	public class AndIsValidTest
	{
		#region DataProvider
		//ncrunch: no coverage start
		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetEmail()
		{
			string validMessage = "Is valid email";
			string inValidMessage = "Is not valid email";

			yield return new object[] { "jonhy@gmail.com", true, validMessage };
			yield return new object[] { "jonhy@gmail.us", true, validMessage };
			yield return new object[] { "jonhy@gmail.io", true, validMessage };
			yield return new object[] { "jonhy@gmail.com.cz", true, validMessage };
			yield return new object[] { "jonhygmail.com", false, inValidMessage };
			yield return new object[] { "jonhy@.io", false, inValidMessage };
			yield return new object[] { "jonhygmailcomcz", false, inValidMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetUrl()
		{
			string validMessage = "Is valid url";
			string inValidMessage = "Is not valid url";

			yield return new object[] { "http://www.google.com", true, validMessage };
			yield return new object[] { "http://www.google.com.br", true, validMessage };
			yield return new object[] { "www.google.com.br", true, validMessage };
			yield return new object[] { "www.google.com", true, validMessage };
			yield return new object[] { "www.google.us", true, validMessage };
			yield return new object[] { "wwwgooglecom", false, inValidMessage };
			yield return new object[] { "12345", false, inValidMessage };
			yield return new object[] { "^^^://nada.com", false, inValidMessage };
			yield return new object[] { "&&&://nada.com", false, inValidMessage };
			yield return new object[] { "http://nadacom", false, inValidMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetCPF()
		{
			string validMessage = "Is valid CPF";
			string inValidMessage = "Is not valid CPF";

			yield return new object[] { "", false, inValidMessage };
			yield return new object[] { "365.885.536-60", false, inValidMessage };
			yield return new object[] { "647.853.472-35", false, inValidMessage };
			yield return new object[] { "813.744.567-64", true, validMessage };
			yield return new object[] { "714.433.548-05", true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetCNPJ()
		{
			string validMessage = "Is valid CNPJ";
			string inValidMessage = "Is not valid CNPJ";

			yield return new object[] { "", false, inValidMessage };
			yield return new object[] { "36.306.667/0001-09", false, inValidMessage };
			yield return new object[] { "93.522.867/0002-98", false, inValidMessage };
			yield return new object[] { "96.836.787/0001-03", true, validMessage };
			yield return new object[] { "53.346.266/0001-57", true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetPhone()
		{
			string validMessage = "Is valid Phone";
			string inValidMessage = "Is not valid Phone";

			yield return new object[] { "", false, inValidMessage };
			yield return new object[] { "00 8888-9999", false, inValidMessage };
			yield return new object[] { "0088889999", false, inValidMessage };
			yield return new object[] { "(00)78888-9999", false, inValidMessage};
			yield return new object[] { "(00) 78888-9999", false, inValidMessage };

			yield return new object[] { "(00) 8888-9999", true, validMessage };
			yield return new object[] { "(00)8888-9999", true, validMessage };
			yield return new object[] { "(00)98888-9999", true, validMessage };
			yield return new object[] { "(00) 98888-9999", true, inValidMessage };
		}
		//ncrunch: no coverage end
		#endregion

		[Theory, MemberData(nameof(GetEmail))]
		[Trait("Patterns Specification Entity AndIsValid", "Email")]
		public void AndIsEmail_With_Execute(string email, bool valid, string message)
		{
			Execute execute = new Execute();
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(email);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsEmail(c => c.Value, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetEmail))]
		[Trait("Patterns Specification Entity AndIsValid", "Email")]
		public void AndIsEmail_Without_Execute(string email, bool valid, string message)
		{
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(email);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsEmail(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetUrl))]
		[Trait("Patterns Specification Entity AndIsValid", "URL")]
		public void AndIsUrl_With_Execute(string url, bool valid, string message)
		{
			Execute execute = new Execute();
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(url);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsURL(c => c.Value, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetUrl))]
		[Trait("Patterns Specification Entity AndIsValid", "URL")]
		public void AndIsUrl_Without_Execute(string url, bool valid, string message)
		{
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(url);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsURL(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetCPF))]
		[Trait("Patterns Specification Entity AndIsValid", "CPF")]
		public void AndIsCPF_With_Execute(string cpf, bool valid, string message)
		{
			Execute execute = new Execute();
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(cpf);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsCPF(c => c.Value, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetCPF))]
		[Trait("Patterns Specification Entity AndIsValid", "CPF")]
		public void AndIsCPF_Without_Execute(string cpf, bool valid, string message)
		{
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(cpf);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsCPF(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetCNPJ))]
		[Trait("Patterns Specification Entity AndIsValid", "CNPJ")]
		public void AndIsCNPJ_With_Execute(string cnpj, bool valid, string message)
		{
			Execute execute = new Execute();
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(cnpj);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsCNPJ(c => c.Value, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetCNPJ))]
		[Trait("Patterns Specification Entity AndIsValid", "CNPJ")]
		public void AndIsCNPJ_Without_Execute(string cnpj, bool valid, string message)
		{
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(cnpj);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsCNPJ(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetPhone))]
		[Trait("Patterns Specification Entity AndIsValid", "Phone")]
		public void AndIsPhone_Without_Execute(string phone, bool valid, string message)
		{
			GenerictValueEntity<string> entity = new GenerictValueEntity<string>(phone);

			SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
				.AndIsPhone(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}
	}
}
