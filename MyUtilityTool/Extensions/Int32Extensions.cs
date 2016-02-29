using System;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.Int32"/> の拡張クラスです。
    /// </summary>
    public static class Int32Extensions
    {
        #region 四則演算系

        /// <summary>
        /// 指定した値を足します。
        /// </summary>
        /// <param name="source">足される数。</param>
        /// <param name="value">足す数。</param>
        /// <returns>和。</returns>
        public static int Add(this int source, int value)
        {
            return source + value;
        }

        /// <summary>
        /// 指定した値を引きます。
        /// </summary>
        /// <param name="source">引かれる数。</param>
        /// <param name="value">引く数。</param>
        /// <returns>差。</returns>
        public static int Subtract(this int source, int value)
        {
            return source - value;
        }

        /// <summary>
        /// 指定した値を掛けます。
        /// </summary>
        /// <param name="source">掛けられる数。</param>
        /// <param name="value">掛ける数。</param>
        /// <returns>積。</returns>
        public static int Multiply(this int source, int value)
        {
            return source * value;
        }

        /// <summary>
        /// 指定した値で割ります。
        /// </summary>
        /// <param name="source">割られる数。</param>
        /// <param name="value">割る数。</param>
        /// <returns>商。</returns>
        public static double Devide(this int source, double value)
        {
            return source / value;
        }

        #endregion

        #region Mathメソッド簡略系

        /// <summary>
        /// 絶対値を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>元の値の絶対値。</returns>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 自然対数を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>対数。</returns>
        public static double Log(this int value)
        {
            return Math.Log(value);
        }

        /// <summary>
        /// 指定した底での対数を返します。
        /// </summary>
        /// <param name="source">元の値。</param>
        /// <param name="newBase">底。</param>
        /// <returns>対数。</returns>
        public static double Log(this int source, double newBase)
        {
            return Math.Log(source, newBase);
        }

        /// <summary>
        /// 底10の対数を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>対数。</returns>
        public static double Log10(this int value)
        {
            return Math.Log10(value);
        }

        /// <summary>
        /// 指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="source">元の値。</param>
        /// <param name="value">累乗する値。</param>
        /// <returns>元の値を累乗する値で累乗した値。</returns>
        public static double Pow(this int source, double value)
        {
            return Math.Pow(source, value);
        }

        /// <summary>
        /// 平方根を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>平方根。</returns>
        public static double Sqrt(this int value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// もう一方の値と比較して、小さい方を返します。
        /// </summary>
        /// <param name="value">比較元の値。</param>
        /// <param name="target">もう一方の値。</param>
        /// <returns>小さい方の値。</returns>
        public static int Min(this int value, int target)
        {
            return Math.Min(value, target);
        }

        /// <summary>
        /// もう一方の値と比較して、大きい方を返します。
        /// </summary>
        /// <param name="value">比較元の値。</param>
        /// <param name="target">もう一方の値。</param>
        /// <returns>大きい方の値。</returns>
        public static int Max(this int value, int target)
        {
            return Math.Max(value, target);
        }

        #endregion

        /// <summary>
        /// パーセント確率の値を取得します。
        /// </summary>
        /// <param name="source">元の値。</param>
        /// <param name="denominator">分母の値。</param>
        /// <returns>パーセント確率の値。</returns>
        public static double Percentage(this int source, int denominator)
        {
            return source.Devide(denominator).Multiply(100D);
        }
    }
}
