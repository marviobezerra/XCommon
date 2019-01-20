using FluentAssertions;
using XCommon.Test.UtilTest.DataSource;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.UtilTest
{
    public class FunctionsTest
    {
        [Theory(DisplayName = "Valid email")]
		[Trait("Common", "Functions")]
		[MemberData(nameof(FunctionDataSource.Email), MemberType = typeof(FunctionDataSource))]
        public void ValidEmail(string email, bool expected, string message)
        {
            var result = Functions.ValidEmail(email);
            result.Should().Be(expected, message + " - " + email);
        }

        [Theory(DisplayName = "Valid url")]
		[Trait("Common", "Functions")]
		[MemberData(nameof(FunctionDataSource.Url), MemberType = typeof(FunctionDataSource))]
        public void ValidUrl(string url, bool expected, string message)
        {
            var result = Functions.ValidUrl(url);
            result.Should().Be(expected, message + " - " + url);
        }

        [Theory(DisplayName = "GetToken (Default)")]
		[Trait("Common", "Functions")]
		[MemberData(nameof(FunctionDataSource.Token), MemberType = typeof(FunctionDataSource))]
        public void GetTokenDefault(int[] parts)
        {
            var token = Functions.GetToken(parts);
            var tokenSplit = token.Split('-');
            tokenSplit.Length.Should().Be(parts.Length, "The number of parts need to match");

            for (var i = 0; i < tokenSplit.Length; i++)
            {
                tokenSplit[i].Length.Should().Be(parts[i], "It needs to respect the toke part size: " + parts[i].ToString());
            }
        }
        
        [Theory(DisplayName = "GetToken (Separator)")]
		[Trait("Common", "Functions")]
		[MemberData(nameof(FunctionDataSource.Token), MemberType = typeof(FunctionDataSource))]
        public void GetTokenSeparator(int[] parts)
        {
            var token = Functions.GetToken(parts);

            var tokenSplit = token.Split('-');
            tokenSplit.Length.Should().Be(parts.Length);

            for (var i = 0; i < tokenSplit.Length; i++)
            {
                tokenSplit[i].Length.Should().Be(parts[i], "It needs to respect the toke part size: " + parts[i].ToString());
            }
        }

        [Theory(DisplayName = "GetRandomString")]
		[Trait("Common", "Functions")]
		[MemberData(nameof(FunctionDataSource.RandomString), MemberType = typeof(FunctionDataSource))]
        public void GetRandomString(int length)
        {
            var result = Functions.GetRandomString(length);
            result.Length.Should().Be(length, "Length doesn't match with the requirement");

        }

        [Theory(DisplayName = "GetRandomNumber")]
		[Trait("Common", "Functions")]
		[MemberData(nameof(FunctionDataSource.RandomNumber), MemberType = typeof(FunctionDataSource))]
        public void GetRandomNumber(int min, int max)
        {
            var result = Functions.GetRandomNumber(min, max);

            result.Should().BeGreaterOrEqualTo(min, "Invalid min: " + min.ToString())
                .And.BeLessOrEqualTo(max, "Invalid max: " + max.ToString());
        }
    }
}
