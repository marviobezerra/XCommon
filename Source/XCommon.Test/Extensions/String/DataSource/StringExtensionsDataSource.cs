using System.Collections.Generic;
using XCommon.Util;

namespace XCommon.Test.Extensions.String.DataSource
{
    public static class StringExtensionsDataSource
    {
        public static IEnumerable<object[]> IsEmptyDataSource
        {
            get
            {
                PairList<string, bool, string> result = new PairList<string, bool, string>();

                result.Add(null, true, "Null value");
                result.Add(string.Empty, true, "string.Empty value");
                result.Add("", true, "Empty value");
                result.Add("    ", true, "Space value");
                result.Add("A", false, "With value");
                result.Add("1", false, "With value");
                result.Add("   1  ", false, "With value");
                result.Add("   -  ", false, "With value");
                result.Add("   .  ", false, "With value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> IsNotEmptyDataSource
        {
            get
            {
                PairList<string, bool, string> result = new PairList<string, bool, string>();

                result.Add(null, false, "Null value");
                result.Add(string.Empty, false, "string.Empty value");
                result.Add("", false, "Empty value");
                result.Add("    ", false, "Space value");
                result.Add("A", true, "With value");
                result.Add("1", true, "With value");
                result.Add("   1  ", true, "With value");
                result.Add("   -  ", true, "With value");
                result.Add("   .  ", true, "With value");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> RemoveAccentDataSource
        {
            get
            {
                PairList<string, string, string> result = new PairList<string, string, string>();

                result.Add("Márvio", "Marvio",  "Needs to be Marvio");
                result.Add("Márvio André Bezerra Silvério", "Marvio Andre Bezerra Silverio", "Marvio Andre Bezerra Silverio");
                result.Add("Mârvio", "Marvio",  "Needs to be Marvio");
                result.Add("Mãrvio", "Marvio",  "Needs to be Marvio");
                result.Add("Màrvio", "Marvio",  "Needs to be Marvio");
                result.Add("ÁÂÃÄÅÇÈÉàáâãäåèéêëìíîïòóôõ", "AAAAACEEaaaaaaeeeeiiiioooo", "Needs to be AAAAACEEaaaaaaeeeeiiiioooo");

                return result.Cast();
            }
        }
    }
}
