using System;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.String"/> の拡張クラスです。
    /// </summary>
    public static class StringExtensions
    {
        #region 型キャスト系

        /// <summary>
        /// 文字列を <see cref="T:System.Boolean"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static bool ToBoolean(this string value)
        {
            return bool.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Boolean"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static bool ToBoolean(this string value, bool defaultValue)
        {
            bool result;
            return bool.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.Int16"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static short ToInt16(this string value)
        {
            return short.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Int16"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static short ToInt16(this string value, short defaultValue)
        {
            short result;
            return short.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.Int32"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static int ToInt32(this string value)
        {
            return int.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Int32"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static int ToInt32(this string value, int defaultValue)
        {
            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.Int64"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static long ToInt64(this string value)
        {
            return long.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Int64"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static long ToInt64(this string value, long defaultValue)
        {
            long result;
            return long.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.Double"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static double ToDouble(this string value)
        {
            return double.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Double"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static double ToDouble(this string value, double defaultValue)
        {
            double result;
            return double.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.Single"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static float ToSingle(this string value)
        {
            return float.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Single"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static float ToSingle(this string value, float defaultValue)
        {
            float result;
            return float.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.Deciamal"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static decimal ToDeciamal(this string value)
        {
            return decimal.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.Deciamal"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static decimal ToDeciamal(this string value, decimal defaultValue)
        {
            decimal result;
            return decimal.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列をChar型に変換する
        /// </summary>
        public static char ToChar(this string value)
        {
            return char.Parse(value);
        }
        /// <summary>
        /// 文字列をChar型に変換する
        /// </summary>
        public static char ToChar(this string value, char defaultValue)
        {
            char result;
            return char.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.DateTime"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static DateTime ToDateTime(this string value)
        {
            return DateTime.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.DateTime"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static DateTime ToDateTime(this string value, DateTime defaultValue)
        {
            DateTime result;
            return DateTime.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を <see cref="T:System.DateTimeOffset"/> に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static DateTimeOffset ToDateTimeOffset(this string value)
        {
            return DateTimeOffset.Parse(value);
        }
        /// <summary>
        /// <para>文字列を <see cref="T:System.DateTimeOffset"/> に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static DateTimeOffset ToDateTimeOffset(this string value, DateTimeOffset defaultValue)
        {
            DateTimeOffset result;
            return DateTimeOffset.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 文字列を指定した列挙体に変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換後の値。</returns>
        public static T ToEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        /// <summary>
        /// <para>文字列を指定した列挙体に変換します。</para>
        /// <para>変換できなかった場合は指定した既定値に変換します。</para>
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <param name="defaultValue">変換できなかった時の既定値。</param>
        /// <returns>変換後の値。</returns>
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            T result;
            return Enum.TryParse(value, out result) ? result : defaultValue;
        }

        #endregion

        #region Stringメソッド簡略型系

        /// <summary>
        /// 指定した文字列の書式項目を、指定した配列内の対応するオブジェクトの文字列形式に置換します。
        /// </summary>
        /// <param name="value">複合書式指定文字列。</param>
        /// <param name="args">0 個以上の書式設定対象オブジェクトを含んだオブジェクト配列。</param>
        /// <returns>書式項目が args の対応するオブジェクトの文字列形式に置換された value のコピー。</returns>
        [Obsolete("この形は旧式です。C# 6.0の文字列保管機能を使用して下さい。")]
        public static string ReplaceFormat(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        /// <summary>
        /// 指定された文字列が null または <see cref="T:System.String.Empty"/> 文字列であるかどうかを示します。
        /// </summary>
        /// <param name="value">テストする文字列。</param>
        /// <returns>value パラメーターが null または空の文字列 ("") の場合は true。それ以外の場合は false。</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 指定された文字列が null または空であるか、空白文字だけで構成されているかどうかを示します。
        /// </summary>
        /// <param name="value">テストする文字列。</param>
        /// <returns>value パラメーターが null または <see cref="T:System.String.Empty"/> であるか、value が空白文字だけで構成されている場合は true。</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        #endregion
    }
}
