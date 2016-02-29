using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyUtilityTool.Extensions
{
    /// <summary>
    /// <see cref="T:System.Collections.Generic.IDictionary"/> の拡張クラスです。
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 2つのディクショナリ型の値を結合し、<see cref="T:System.Collections.Generic.IDictionary"/> として変換します。
        /// </summary>
        /// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
        /// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
        /// <param name="source">1つ目のディクショナリ。</param>
        /// <param name="value">2つ目のディクショナリ。</param>
        /// <returns>結合された <see cref="T:System.Collections.Generic.IDictionary"/> 。</returns>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return source.Concat(value).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// <para>ディクショナリ内を指定したキーで検索し、対象となる値を取得します。</para>
        /// <para>検索するキーに対する値が見るからない場合、既定値が返されます。</para>
        /// </summary>
        /// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
        /// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
        /// <param name="source">検索対象のディクショナリ。</param>
        /// <param name="key">検索するキー。</param>
        /// <returns>検索結果。</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            return GetValueOrDefault(source, key, default(TValue));
        }

        /// <summary>
        /// <para>ディクショナリ内を指定したキーで検索し、対象となる値を取得します。</para>
        /// <para>検索するキーに対する値が見るからない場合、指定した既定値が返されます。</para>
        /// </summary>
        /// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
        /// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
        /// <param name="source">検索対象のディクショナリ。</param>
        /// <param name="key">検索するキー。</param>
        /// <param name="defaultValue">検索するキーに対する値が見つからなかった場合の既定値。</param>
        /// <returns>検索結果。</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            TValue value;
            return source.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
        /// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
        /// <param name="source">変換元となるディクショナリ。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> 。</returns>
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new ReadOnlyDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// <see cref="T:System.Collections.Generic.SortedDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
        /// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
        /// <param name="source">変換元となるディクショナリ。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Generic.SortedDictionary"/> 。</returns>
        public static SortedDictionary<TKey, TValue> AsSorted<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new SortedDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// <see cref="T:System.Collections.Concurrent.ConcurrentDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
        /// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
        /// <param name="source">変換元となるディクショナリ。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Concurrent.ConcurrentDictionary"/> 。</returns>
        public static ConcurrentDictionary<TKey, TValue> AsConcurrent<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new ConcurrentDictionary<TKey, TValue>(source);
        }
    }
}
