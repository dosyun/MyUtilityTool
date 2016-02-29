using MyUtilityTool.Attributes;
using System;
using System.Security.Cryptography;
using System.Threading;

namespace MyUtilityTool.Utilities
{
    /// <summary>
    /// 一意のシードから生成された <see cref="T:System.Radom"/> を提供するクラスです。
    /// </summary>
    [Preload]
    public class UniqueSeedRandom
    {
        private static readonly Random _random;
        private static readonly ThreadLocal<Random> _threadLocalRandom;

        /// <summary>
        /// 一意のシードから生成された <see cref="T:System.Radom"/> を取得します。
        /// </summary>
        public static Random Current => _random;

        /// <summary>
        /// 一意のシードから生成されたスレッドセーフな <see cref="T:System.Radom"/> を取得します。
        /// </summary>
        public static Random ThreadLocal => _threadLocalRandom.Value;

        static UniqueSeedRandom()
        {
            var randomFactory = new Func<Random>(() =>
            {
                var buffer = new byte[sizeof(int)];
                using (var rng = new RNGCryptoServiceProvider())
                    rng.GetBytes(buffer);
                var seed = BitConverter.ToInt32(buffer, 0);
                return new Random(seed);
            });

            _random = randomFactory();
            _threadLocalRandom = new ThreadLocal<Random>(() => _random);
        }
    }
}
