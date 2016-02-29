using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilityTool.Utilities
{
    public class RandomUtil
    {
        private const string BaseString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int default_length = 5;

        public static string RandomNumber(int len)
        {
            return GenerateRandom(len, true);
        }
        public static string RandomNumber()
        {
            return RandomNumber(default_length);
        }

        public static string RandomString(int len)
        {
            return GenerateRandom(len, false);
        }
        public static string RandomString()
        {
            return RandomString(default_length);
        }

        private static string GenerateRandom(int len, bool isInt)
        {
            StringBuilder rtn = new StringBuilder();
            Random r = UniqueRandom;
            int baselen = BaseString.Length;
            while (rtn.Length < len)
            {
                if (isInt)
                    rtn.Append(r.Next(0, 10));
                else
                    rtn.Append(BaseString.Substring(r.Next(0, baselen), 1));
            }
            return rtn.ToString();
        }

        public static string GuidCode
        {
            get
            {
                return System.Guid.NewGuid().ToString("N");
            }
        }

        public static Random UniqueRandom
        {
            get
            {
                return new Random(RandomSeed);
            }
        }

        private static int RandomSeed
        {
            get
            {
                byte[] bytes = new byte[4];
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                rng.GetBytes(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }
        }
      

        /// <summary>
        /// 抽選を実施する（抽選確率に依存せずスケールする）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">intには当選する割合、Tには当選時に返却したいオブジェクトを指定する</param>
        /// <returns></returns>
        public static T DrawLots<T>(Dictionary<T, int> items)
        {
            //<Tuple(from, to), T>の辞書を作成
            Dictionary<Tuple<int, int>, T> table = new Dictionary<Tuple<int, int>, T>();

            int i = 0;
            foreach (KeyValuePair<T, int> kv in items)
            {
                table[new Tuple<int, int>(i, i + kv.Value)] = kv.Key;
                i += kv.Value;
            }
            int random = RandomUtil.UniqueRandom.Next(i);

            //from <= random && random < toを満たす要素を取得
            return table.Where(kv => kv.Key.Item1 <= random && random < kv.Key.Item2).First().Value;
        }

        /// <summary>
        /// パラメータで渡された整数を上限として整数のリストをランダム作成
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<int> RandomList(int n)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                int ix = UniqueRandom.Next(i);
                list.Insert(ix, i);
            }
            return list;
        }
    }
}
