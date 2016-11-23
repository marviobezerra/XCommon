using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Util;

namespace XCommon.Test.UtilTest.DataSource
{
    public static class FunctionDataSource
    {
        public static IEnumerable<object[]> Email
        {
            get
            {
                PairList<string, bool> result = new PairList<string, bool>();

                result.Add(null, false, "Null string");
                result.Add(string.Empty, false, "Empty string");
                result.Add("", false, "Empty string");
                result.Add("   ", false, "White space string");
                result.Add("1@.com", false, "Invalid email");
                result.Add("jonhy@.com", false, "Invalid email");
                result.Add("jonhy", false, "Invalid email");
                result.Add("@goggle.com", false, "Invalid email");
                result.Add("marvio.bezerra@gmail.com", true, "Valid email");
                result.Add("marvio.bezerra@gmail.com.br", true, "Valid email");
                result.Add("marvio.bezerra-01@gmail.com.br", true, "Valid email");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> Url
        {
            get
            {
                PairList<string, bool> result = new PairList<string, bool>();

                result.Add(null, false, "Null string");
                result.Add(string.Empty, false, "Empty string");
                result.Add("", false, "Empty string");
                result.Add("   ", false, "White space string");
                result.Add("teste", false, "Invalid url");
                result.Add("teste\\com", false, "Invalid url");
                result.Add("teste-com", false, "Invalid url");

                result.Add("www.teste.com", false, "Invalid url");
                result.Add("teste.com", false, "Invalid url");
                result.Add("teste.com/nada", false, "Invalid url");
                result.Add("www.teste.com/nada", false, "Invalid url");
                result.Add("www.teste.com/nada", false, "Invalid url");
                result.Add("127.0.0.1", false, "Invalid url");

                result.Add("http://teste.com", true, "Valid url");
                result.Add("https://teste.com", true, "Valid url");
                result.Add("http://www.teste.com", true, "Valid url");
                result.Add("https://www.teste.com", true, "Valid url");
                result.Add("http://127.0.0.1", true, "Valid url");

                return result.Cast();
            }
        }
                
        public static IEnumerable<object[]> Token
        {
            get
            {
                PairList<int[], char> result = new PairList<int[], char>();

                result.Add(new int[] { 5, 4, 5 }, '+', "5, 4, 5");
                result.Add(new int[] { 8, 2, 9 }, '@', "8, 2, 9");
                result.Add(new int[] { 8, 8 }, '*', "8, 8");
                result.Add(new int[] { 8, 8, 8, 8, 8 }, '^', "8, 8, 8, 8, 8");
                result.Add(new int[] { 8, 7, 6, 5, 4 }, '$', "8, 7, 6, 5, 4");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> RandomString
        {
            get
            {
                PairList<string, bool> result = new PairList<string, bool>();

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> RandomNumber
        {
            get
            {
                PairList<string, bool> result = new PairList<string, bool>();

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> MD5
        {
            get
            {
                PairList<string, bool> result = new PairList<string, bool>();

                return result.Cast();
            }
        }
    }
}
