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
        // Extrema系の拡張メソッドをここにまとめる

        /// <summary>
        /// 指定したキーの最大値の要素を取得します。
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <returns>指定したキーの最大だったうちの最初の要素。</returns>
        public static TElement MaxBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            return MaxBy(source, keySelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// 指定された比較子を使用して、キーの最大値の要素を取得します。
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <param name="comparer">キー同士の比較子。</param>
        /// <returns>指定したキーの最大だったうちの最初の要素。</returns>
        public static TElement MaxBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return ExtremaBy(source, keySelector, comparer.Compare, false);
        }

        /// <summary>
        /// <para>指定したキーの最大値の要素を取得します。</para>
        /// <para>シーケンスに要素がない場合は既定値を返します。</para>
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <returns>指定したキーの最大だったうちの最初の要素。</returns>
        public static TElement MaxByOrDefault<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            return MaxByOrDefault(source, keySelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// <para>指定された比較子を使用して、キーの最大値の要素を取得します。</para>
        /// <para>シーケンスに要素がない場合は既定値を返します。</para>
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <param name="comparer">キー同士の比較子。</param>
        /// <returns>指定したキーの最大だったうちの最初の要素。</returns>
        public static TElement MaxByOrDefault<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return ExtremaBy(source, keySelector, comparer.Compare, true);
        }

        /// <summary>
        /// 指定された比較子を使用して、キーの最小値の要素を取得します。
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <returns>指定したキーの最小だったうちの最初の要素。</returns>
        public static TElement MinBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            return MinBy(source, keySelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// 指定された比較子を使用して、キーの最小値の要素を取得します。
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <param name="comparer">キー同士の比較子。</param>
        /// <returns>指定したキーの最小だったうちの最初の要素。</returns>
        public static TElement MinBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return ExtremaBy(source, keySelector, (x, y) => -comparer.Compare(x, y), false);
        }

        /// <summary>
        /// <para>指定された比較子を使用して、キーの最小値の要素を取得します。</para>
        /// <para>シーケンスに要素がない場合は既定値を返します。</para>
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <returns>指定したキーの最小だったうちの最初の要素。</returns>
        public static TElement MinByOrDefault<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            return MinByOrDefault(source, keySelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// <para>指定された比較子を使用して、キーの最小値の要素を取得します。</para>
        /// <para>シーケンスに要素がない場合は既定値を返します。</para>
        /// </summary>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">対象のシーケンス。</param>
        /// <param name="keySelector">大小を比較するためのキー選択。</param>
        /// <param name="comparer">キー同士の比較子。</param>
        /// <returns>指定したキーの最小だったうちの最初の要素。</returns>
        public static TElement MinByOrDefault<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return ExtremaBy(source, keySelector, (x, y) => -comparer.Compare(x, y), true);
        }

        // 渡されたラムダ式から極値を取得する。
        private static TElement ExtremaBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector, Func<TKey, TKey, int> comparer, bool requiredDefaultIfEmpty)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            using (var e = source.GetEnumerator())
            {
                // 要素がなかった場合
                if (!e.MoveNext())
                {
                    if (requiredDefaultIfEmpty)
                        return default(TElement);

                    throw new InvalidOperationException("シーケンスに要素が含まれていません。");
                }

                var current = e.Current;
                var currentKey = keySelector(current);
                while (e.MoveNext())
                {
                    var next = e.Current;
                    var nextKey = keySelector(next);
                    if (0 <= comparer(currentKey, nextKey))
                        continue;

                    current = next;
                    currentKey = nextKey;
                }

                return current;
            }
        }
    }
}
