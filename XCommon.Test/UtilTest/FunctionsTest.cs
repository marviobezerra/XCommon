using FluentAssertions;
using XCommon.Test.UtilTest.DataSource;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.UtilTest
{
    public class FunctionsTest
    {
        [Theory(DisplayName = "Valid email")]
        [MemberData(nameof(FunctionDataSource.Email), MemberType = typeof(FunctionDataSource))]
        public void ValidEmail(string email, bool expected, string message)
        {
            var result = Functions.ValidEmail(email);
            result.Should().Be(expected, message + " - " + email);
        }

        [Theory(DisplayName = "Valid url")]
        [MemberData(nameof(FunctionDataSource.Url), MemberType = typeof(FunctionDataSource))]
        public void ValidUrl(string url, bool expected, string message)
        {
            var result = Functions.ValidUrl(url);
            result.Should().Be(expected, message + " - " + url);
        }

        [Theory(DisplayName = "GetToken (Default)")]
        [MemberData(nameof(FunctionDataSource.Token), MemberType = typeof(FunctionDataSource))]
        public void GetTokenDefault(int[] parts, char separator, string message)
        {
            var token = Functions.GetToken(parts);
            var tokenSplit = token.Split('-');
            tokenSplit.Length.Should().Be(parts.Length, "The number of parts need to match");

            for (int i = 0; i < tokenSplit.Length; i++)
            {
                tokenSplit[i].Length.Should().Be(parts[i], "It needs to respect the toke part size: " + parts[i].ToString());
            }

        }
        
        [Theory(DisplayName = "GetToken (Separator)")]
        [MemberData(nameof(FunctionDataSource.Token), MemberType = typeof(FunctionDataSource))]
        public void GetTokenSeparator(int[] parts, char separator, string message)
        {
            var token = Functions.GetToken(separator, parts);

            var tokenSplit = token.Split(separator);
            tokenSplit.Length.Should().Be(parts.Length, "The number of parts need to match: " + message);

            for (int i = 0; i < tokenSplit.Length; i++)
            {
                tokenSplit[i].Length.Should().Be(parts[i], "It needs to respect the toke part size: " + parts[i].ToString());
            }
        }

        [Theory(Skip = "Not implemented", DisplayName = "GetRandomString")]
        [MemberData(nameof(FunctionDataSource.RandomString), MemberType = typeof(FunctionDataSource))]
        public void GetRandomString()
        {

        }

        [Theory(Skip = "Not implemented", DisplayName = "GetRandomNumber")]
        [MemberData(nameof(FunctionDataSource.RandomNumber), MemberType = typeof(FunctionDataSource))]
        public void GetRandomNumber()
        {

        }

        [Theory(Skip = "Not implemented", DisplayName = "GetMD5")]
        [MemberData(nameof(FunctionDataSource.MD5), MemberType = typeof(FunctionDataSource))]
        public void GetMD5()
        {

        }
    }
}
