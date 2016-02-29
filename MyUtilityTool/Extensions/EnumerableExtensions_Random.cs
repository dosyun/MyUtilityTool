using MyUtilityTool.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.Collections.Generic.IEnumerable"/> の拡張クラスです。
    /// </summary>
    public static partial class EnumerableExtensions
    {
        // Random系の拡張メソッドをここにまとめる

        /// <summary>
        /// <para>シーケンスを無作為に並び替えしたものを返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <returns>無作為に並び替えられたシーケンス。</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return Shuffle(source, UniqueSeedRandom.ThreadLocal);
        }

        /// <summary>
        /// <para>シーケンスを無作為に並び替えしたものを返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <returns>無作為に並び替えられたシーケンス。</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (random == null)
                throw new ArgumentNullException(nameof(random));

            var buffer = source.ToArray();

            for (var i = buffer.Length - 1; i > 0; i--)
            {
                var j = random.Next(0, i + 1);

                yield return buffer[j];
                buffer[j] = buffer[i];
            }

            if (buffer.Length != 0)
                yield return buffer[0];
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された順序に並び替えしたものを返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// <para>シーケンスの要素数上限の制約が用いられます。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static IEnumerable<T> OrderByWeight<T>(this IEnumerable<T> source, Func<T, int> weightSelector)
        {
            return OrderByWeight(source, weightSelector, true);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された順序に並び替えしたものを返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="hasLimit">要素数上限の制約を用いるかどうか。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static IEnumerable<T> OrderByWeight<T>(this IEnumerable<T> source, Func<T, int> weightSelector, bool hasLimit)
        {
            return OrderByWeight(source, weightSelector, UniqueSeedRandom.ThreadLocal);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された順序に並び替えしたものを返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// <para>シーケンスの要素数上限の制約が用いられます。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static IEnumerable<T> OrderByWeight<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random)
        {
            return OrderByWeight(source, weightSelector, random, true);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された順序に並び替えしたものを返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <param name="hasLimit">要素数上限の制約を用いるかどうか。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static IEnumerable<T> OrderByWeight<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random, bool hasLimit)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (weightSelector == null)
                throw new ArgumentNullException(nameof(weightSelector));

            if (random == null)
                throw new ArgumentNullException(nameof(random));

            var seed = new WeightedSampleSeed<T>(source, weightSelector, random);
            if (seed.ItemNum < 1)
                yield break;

            var counter = 0;
            while (true)
            {
                if (hasLimit && EnumerableCountLimit < ++counter)
                    throw new ArgumentException($"シーケンスの要素数が{EnumerableCountLimit}件を超えています。");

                yield return seed.Sample();
            }
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSample<T>(this IEnumerable<T> source, Func<T, int> weightSelector)
        {
            return WeightedSample(source, weightSelector, true);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="hasLimit">要素数上限の制約を用いるかどうか。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSample<T>(this IEnumerable<T> source, Func<T, int> weightSelector, bool hasLimit)
        {
            return WeightedSample(source, weightSelector, UniqueSeedRandom.ThreadLocal, hasLimit);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSample<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random)
        {
            return WeightedSample(source, weightSelector, random, true);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <param name="hasLimit">要素数上限の制約を用いるかどうか。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSample<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random, bool hasLimit)
        {
            return OrderByWeight(source, weightSelector, random, hasLimit).First();
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>シーケンスに要素が存在しない場合は既定値を返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSampleOrDefault<T>(this IEnumerable<T> source, Func<T, int> weightSelector)
        {
            return WeightedSample(source, weightSelector, true);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>シーケンスに要素が存在しない場合は既定値を返します。</para>
        /// <para>ランダム生成子は一意のシード値から作られたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="hasLimit">要素数上限の制約を用いるかどうか。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSampleOrDefault<T>(this IEnumerable<T> source, Func<T, int> weightSelector, bool hasLimit)
        {
            return WeightedSample(source, weightSelector, UniqueSeedRandom.ThreadLocal, hasLimit);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>シーケンスに要素が存在しない場合は既定値を返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSampleOrDefault<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random)
        {
            return WeightedSample(source, weightSelector, random, true);
        }

        /// <summary>
        /// <para>シーケンスから指定された重み付けで復元抽選された抽選結果を返します。</para>
        /// <para>シーケンスに要素が存在しない場合は既定値を返します。</para>
        /// <para>ランダム生成子は指定されたものを用います。</para>
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="weightSelector">重み付けの方法。</param>
        /// <param name="random">ランダム生成子。</param>
        /// <param name="hasLimit">要素数上限の制約を用いるかどうか。</param>
        /// <returns>並び替えられたシーケンス。</returns>
        public static T WeightedSampleOrDefault<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random, bool hasLimit)
        {
            return OrderByWeight(source, weightSelector, random, hasLimit).FirstOrDefault();
        }

        // 重み付け復元抽選
        private class WeightedSampleSeed<T>
        {
            private readonly Random _random;
            private readonly int _totalWeight;
            private readonly List<T> _items;
            private readonly List<int> _weightBounds;

            internal int ItemNum => _items.Count;

            internal WeightedSampleSeed(IEnumerable<T> source, Func<T, int> weightSelector, Random random)
            {
                _random = random;

                _items = new List<T>();
                _weightBounds = new List<int>();
                _totalWeight = 0;

                foreach (var x in source)
                {
                    var weight = weightSelector(x);
                    if (weight <= 0)
                        continue;

                    checked { _totalWeight += weight; }

                    _items.Add(x);
                    _weightBounds.Add(_totalWeight);
                }
            }

            internal T Sample()
            {
                var randomValue = _random.Next(1, _totalWeight + 1);
                var index = _weightBounds.BinarySearch(randomValue);

                if (index < 0)
                    index = ~index;

                return _items[index];
            }
        }
    }
}
