using MyUtilityTool.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.Collections.Generic.IEnumerable"/> の拡張クラスです。
    /// </summary>
    public static partial class EnumerableExtensions
    {
        // シーケンスの要素数上限
        private const int EnumerableCountLimit = 1000;

        /// <summary>
        /// シーケンスを一つの文字列として結合します。
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <returns>シーケンスを結合した文字列、</returns>
        public static string Combine<T>(this IEnumerable<T> source)
        {
            return Combine(source, "");
        }

        /// <summary>
        /// シーケンスを指定した区切り文字を用いて、一つの文字列として結合します。
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <param name="separator">区切り文字。</param>
        /// <returns>シーケンスを結合した文字列。</returns>
        public static string Combine<T>(this IEnumerable<T> source, string separator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return string.Join(separator, source);
        }

        /// <summary>
        /// シーケンスを読み取り専用のコレクションに変換します。
        /// </summary>
        /// <typeparam name="T">シーケンスの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <returns>変換されたコレクション。</returns>
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            return source.ToList().AsReadOnly();
        }

        /// <summary>
        /// 指定されたキーセレクター関数に従って、 <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TSource">シーケンスの型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <param name="keySelector">各要素からキーを抽出する関数。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> 。</returns>
        public static IReadOnlyDictionary<TKey, TSource> ToReadOnlyDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ToDictionary(keySelector).AsReadOnly();
        }

        /// <summary>
        /// 指定されたキーセレクター関数と要素セレクター関数に従って、 <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TSource">シーケンスの型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <param name="keySelector">各要素からキーを抽出する関数。</param>
        /// <param name="elementSelector">各要素から結果の要素値を生成する変換関数。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> 。</returns>
        public static IReadOnlyDictionary<TKey, TElement> ToReadOnlyDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return source.ToDictionary(keySelector, elementSelector).AsReadOnly();
        }

        /// <summary>
        /// 指定されたキーセレクター関数とキーの比較子に従って、 <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TSource">シーケンスの型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <param name="keySelector">各要素からキーを抽出する関数。</param>
        /// <param name="comparer">キーの比較子。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> 。</returns>
        public static IReadOnlyDictionary<TKey, TSource> ToReadOnlyDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary(keySelector, comparer).AsReadOnly();
        }

        /// <summary>
        /// 指定されたキーセレクター関数と要素セレクター関数、キーの比較子に従って、 <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> に変換します。
        /// </summary>
        /// <typeparam name="TSource">シーケンスの型。</typeparam>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TElement">要素の型。</typeparam>
        /// <param name="source">操作するシーケンス。</param>
        /// <param name="keySelector">各要素からキーを抽出する関数。</param>
        /// <param name="elementSelector">各要素から結果の要素値を生成する変換関数。</param>
        /// <param name="comparer">キーの比較子。</param>
        /// <returns>変換後の <see cref="T:System.Collections.Generic.IReadOnlyDictionary"/> 。</returns>
        public static IReadOnlyDictionary<TKey, TElement> ToReadOnlyDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary(keySelector, elementSelector, comparer).AsReadOnly();
        }

        /// <summary>
        /// シーケンスを <see cref="T:System.Data.DataTable"/> に変換します。
        /// </summary>
        /// <typeparam name="T">シーケンスの要素の型。</typeparam>
        /// <param name="values">対象のシーケンス。</param>
        /// <returns>対象のシーケンスから変換された <see cref="T:System.Data.DataTable"/> 。</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> values) where T : class
        {
            var valueType = typeof(T);
            var tableName = valueType.Name;
            var columns = valueType.GetProperties();

            using (var dataTable = new DataTable())
            {
                dataTable.TableName = tableName;
                dataTable.Columns.AddRange(columns.Select(x =>
                {
                    var columnType = x.PropertyType;
                    if (x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        columnType = Nullable.GetUnderlyingType(x.PropertyType);

                    return new DataColumn(x.Name, columnType);
                }).ToArray());

                return dataTable;
            }
        }

        /// <summary>
        /// ページング対象シーケンスの最大要素数
        /// </summary>
        const int PaginateMaxElementCount = 2000;

        /// <summary>
        /// シーケンスをページングして返す
        /// </summary>
        /// <param name="source">対象となるシーケンス</param>
        /// <param name="pageSize">1ページに表示する要素数</param>
        /// <param name="currentPage">現在のページ数</param>
        /// <returns>ページングされたシーケンス</returns>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int pageSize, int currentPage)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Count() > PaginateMaxElementCount)
                throw new ArgumentException(string.Format("ページング対象のシーケンスの要素数が{0}件を超えています。", PaginateMaxElementCount));

            return source.Skip(pageSize * (currentPage - 1)).Take(pageSize);
        }

        /// <summary>
        /// シーケンスをページングして返す
        /// </summary>
        /// <param name="source">対象となるシーケンス</param>
        /// <param name="pageSize">1ページに表示する要素数</param>
        /// <param name="currentPage">現在のページ数</param>
        /// <param name="totalCount">対象となるシーケンスの件数</param>
        /// <returns>ページングされたシーケンス</returns>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int pageSize, int currentPage, out int totalCount)
        {
            totalCount = source.Count();
            return source.Paginate(pageSize, currentPage);
        }



        /// <summary>
        /// シーケンスからcountに指定した要素数の配列を生成し、yield returnする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">処理対象のシーケンス</param>
        /// <param name="count">yield returnする配列の要素数</param>
        /// <returns></returns>
        public static IEnumerable<T[]> Buffer<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (count <= 0) throw new ArgumentOutOfRangeException("count");

            return BufferCore(source, count);
        }

        static IEnumerable<T[]> BufferCore<T>(this IEnumerable<T> source, int count)
        {
            var buffer = new T[count];
            var index = 0;
            foreach (var item in source)
            {
                buffer[index++] = item;
                if (index == count)
                {
                    yield return buffer;
                    index = 0;
                    buffer = new T[count];
                }
            }

            if (index != 0)
            {
                var dest = new T[index];
                Array.Copy(buffer, dest, index);
                yield return dest;
            }
        }


        //Listライクに直接ForEachを呼べるように拡張
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        //Distinctにラムダ式を直接渡せるように拡張
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.Distinct(new CompareSelector<T, TKey>(selector));
        }

        internal class CompareSelector<T, TKey> : IEqualityComparer<T>
        {
            private Func<T, TKey> selector;

            public CompareSelector(Func<T, TKey> selector)
            {
                this.selector = selector;
            }

            public bool Equals(T x, T y)
            {
                return selector(x).Equals(selector(y));
            }

            public int GetHashCode(T obj)
            {
                return selector(obj).GetHashCode();
            }
        }

    }
}
