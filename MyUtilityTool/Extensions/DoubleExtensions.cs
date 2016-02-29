using System;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.Double"/> の拡張クラスです。
    /// </summary>
    public static class DoubleExtensions
    {
        #region 四則演算系

        /// <summary>
        /// 指定した値を足します。
        /// </summary>
        /// <param name="source">足される数。</param>
        /// <param name="value">足す数。</param>
        /// <returns>和。</returns>
        public static double Add(this double source, double value)
        {
            return source + value;
        }

        /// <summary>
        /// 指定した値を引きます。
        /// </summary>
        /// <param name="source">引かれる数。</param>
        /// <param name="value">引く数。</param>
        /// <returns>差。</returns>
        public static double Subtract(this double source, double value)
        {
            return source - value;
        }

        /// <summary>
        /// 指定した値を掛けます。
        /// </summary>
        /// <param name="source">掛けられる数。</param>
        /// <param name="value">掛ける数。</param>
        /// <returns>積。</returns>
        public static double Multiply(this double source, double value)
        {
            return source * value;
        }

        /// <summary>
        /// 指定した値で割ります。
        /// </summary>
        /// <param name="source">割られる数。</param>
        /// <param name="value">割る数。</param>
        /// <returns>商。</returns>
        public static double Devide(this double source, double value)
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
        public static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 整数へ切り下げて返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>整数に切り下げられた値。</returns>
        public static int Ceiling(this double value)
        {
            return (int)Math.Ceiling(value);
        }

        /// <summary>
        /// 整数へ切り上げて返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>整数に切り上げられた数。</returns>
        public static int Floor(this double value)
        {
            return (int)Math.Floor(value);
        }

        /// <summary>
        /// 自然対数を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>対数。</returns>
        public static double Log(this double value)
        {
            return Math.Log(value);
        }

        /// <summary>
        /// 指定した底での対数を返します。
        /// </summary>
        /// <param name="source">元の値。</param>
        /// <param name="newBase">底。</param>
        /// <returns>対数。</returns>
        public static double Log(this double source, double newBase)
        {
            return Math.Log(source, newBase);
        }

        /// <summary>
        /// 底10の対数を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>対数。</returns>
        public static double Log10(this double value)
        {
            return Math.Log10(value);
        }

        /// <summary>
        /// 指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="source">元の値。</param>
        /// <param name="value">累乗する値。</param>
        /// <returns>元の値を累乗する値で累乗した値。</returns>
        public static double Pow(this double source, double value)
        {
            return Math.Pow(source, value);
        }

        /// <summary>
        /// 最も近い整数値に丸めます。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>丸められた整数値。</returns>
        public static double Round(this double value)
        {
            return Math.Round(value);
        }

        /// <summary>
        /// 指定した小数部の桁数に丸めます。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <param name="digits">小数部の桁数。</param>
        /// <returns>丸められた値。</returns>
        public static double Round(this double value, int digits)
        {
            return Math.Round(value, digits);
        }

        /// <summary>
        /// 平方根を返します。
        /// </summary>
        /// <param name="value">元の値。</param>
        /// <returns>平方根。</returns>
        public static double Sqrt(this double value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// もう一方の値と比較して、小さい方を返します。
        /// </summary>
        /// <param name="value">比較元の値。</param>
        /// <param name="target">もう一方の値。</param>
        /// <returns>小さい方の値。</returns>
        public static double Min(this double value, double target)
        {
            return Math.Min(value, target);
        }

        /// <summary>
        /// もう一方の値と比較して、大きい方を返します。
        /// </summary>
        /// <param name="value">比較元の値。</param>
        /// <param name="target">もう一方の値。</param>
        /// <returns>大きい方の値。</returns>
        public static double Max(this double value, double target)
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
        public static double Percentage(this double source, double denominator)
        {
            return source.Devide(denominator).Multiply(100D);
        }
    }
}
