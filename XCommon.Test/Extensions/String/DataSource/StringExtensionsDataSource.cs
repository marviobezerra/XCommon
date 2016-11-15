using System.Collections.Generic;
using XCommon.UnitTest;
using XCommon.Util;

namespace XCommon.Test.Extensions.String.DataSource
{
    public static class StringExtensionsDataSource
    {
        public static IEnumerable<object[]> IsEmptyDataSource
        {
            get
            {
                List<DataItem<string>> result = new List<DataItem<string>>();

                result.Add(new DataItem<string>(null, true, "Null value"));
                result.Add(new DataItem<string>(string.Empty, true, "string.Empty value"));
                result.Add(new DataItem<string>("", true, "Empty value"));
                result.Add(new DataItem<string>("    ", true, "Space value"));
                result.Add(new DataItem<string>("A", false, "With value"));
                result.Add(new DataItem<string>("1", false, "With value"));
                result.Add(new DataItem<string>("   1  ", false, "With value"));
                result.Add(new DataItem<string>("   -  ", false, "With value"));
                result.Add(new DataItem<string>("   .  ", false, "With value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> IsNotEmptyDataSource
        {
            get
            {
                List<DataItem<string>> result = new List<DataItem<string>>();

                result.Add(new DataItem<string>(null, false, "Null value"));
                result.Add(new DataItem<string>(string.Empty, false, "string.Empty value"));
                result.Add(new DataItem<string>("", false, "Empty value"));
                result.Add(new DataItem<string>("    ", false, "Space value"));
                result.Add(new DataItem<string>("A", true, "With value"));
                result.Add(new DataItem<string>("1", true, "With value"));
                result.Add(new DataItem<string>("   1  ", true, "With value"));
                result.Add(new DataItem<string>("   -  ", true, "With value"));
                result.Add(new DataItem<string>("   .  ", true, "With value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> RemoveAccentDataSource
        {
            get
            {
                List<DataItem<Pair<string, string>>> result = new List<DataItem<Pair<string, string>>>();

                result.Add(new DataItem<Pair<string, string>>(new Pair<string, string>("Márvio", "Marvio"), true, "Needs to be Marvio"));
                result.Add(new DataItem<Pair<string, string>>(new Pair<string, string>("Márvio André Bezerra Silvério", "Marvio Andre Bezerra Silverio"), true, "Needs to be Marvio"));
                result.Add(new DataItem<Pair<string, string>>(new Pair<string, string>("Mârvio", "Marvio"), true, "Needs to be Marvio"));
                result.Add(new DataItem<Pair<string, string>>(new Pair<string, string>("Mãrvio", "Marvio"), true, "Needs to be Marvio"));
                result.Add(new DataItem<Pair<string, string>>(new Pair<string, string>("Màrvio", "Marvio"), true, "Needs to be Marvio"));
                result.Add(new DataItem<Pair<string, string>>(new Pair<string, string>("ÁÂÃÄÅÇÈÉàáâãäåèéêëìíîïòóôõ", "AAAAACEEaaaaaaeeeeiiiioooo"), true, "Needs to be Marvio"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
