using System;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.IComparable"/> の拡張クラスです。
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// 現在の値が指定した最小値と最大値の範囲に含まれているかどうかを示します。
        /// </summary>
        /// <typeparam name="T">比較する型。</typeparam>
        /// <param name="value">現在の値。</param>
        /// <param name="min">最小値。</param>
        /// <param name="max">最大値。</param>
        /// <returns>現在の値が最小値と最大値の範囲にある場合 true。それ以外の場合は false。</returns>
        public static bool InBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return 0 <= value.CompareTo(min) && value.CompareTo(max) <= 0;
        }

        /// <summary>
        /// 現在の値が比較対象の値以上かどうかを示します。
        /// </summary>
        /// <typeparam name="T">比較する型。</typeparam>
        /// <param name="value">現在の値。</param>
        /// <param name="compareValue">比較対象の値。</param>
        /// <returns>現在の値が比較対象の値以上の場合は true。それ以外の場合は false。</returns>
        public static bool IsGreaterThanOrEqual<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return value.CompareTo(compareValue) <= 0;
        }

        /// <summary>
        /// 現在の値が比較対象の値より大きいかどうかを示します。
        /// </summary>
        /// <typeparam name="T">比較する型。</typeparam>
        /// <param name="value">現在の値。</param>
        /// <param name="compareValue">比較対象の値。</param>
        /// <returns>現在の値が比較対象の値より大きい場合は true。それ以外の場合は false。</returns>
        public static bool IsGreaterThan<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return value.CompareTo(compareValue) < 0;
        }

        /// <summary>
        /// 現在の値が比較対象の値以下かどうかを示します。
        /// </summary>
        /// <typeparam name="T">比較する型。</typeparam>
        /// <param name="value">現在の値。</param>
        /// <param name="compareValue">比較対象の値。</param>
        /// <returns>現在の値が比較対象の値以下の場合は true。それ以外の場合は false。</returns>
        public static bool IsLessThanOrEqual<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return 0 <= value.CompareTo(compareValue);
        }

        /// <summary>
        /// 現在の値が比較対象の値より小さいかどうかを示します。
        /// </summary>
        /// <typeparam name="T">比較する型。</typeparam>
        /// <param name="value">現在の値。</param>
        /// <param name="compareValue">比較対象の値。</param>
        /// <returns>現在の値が比較対象の値より小さい場合は true。それ以外の場合は false。</returns>
        public static bool IsLessThan<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return 0 < value.CompareTo(compareValue);
        }
    }
}
