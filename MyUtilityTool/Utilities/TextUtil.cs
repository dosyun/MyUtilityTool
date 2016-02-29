using Microsoft.VisualBasic;

namespace MyUtilityTool.Utilities
{
    class TextUtil
    {

        /// <summary>
        /// アジアロケールの文字列に対し全角半角変換を行う
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToNarrow(string value)
        {
            return Strings.StrConv(value, VbStrConv.Narrow);
        }

        /// <summary>
        /// アジアロケールの文字列に対し全角半角変換を行う
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToWide(string value)
        {
            return Strings.StrConv(value, VbStrConv.Wide);
        }

        /// <summary>
        /// アジアロケールの文字列に対しカタカナ変換を行う
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToKatakana(string value)
        {
            return Strings.StrConv(value, VbStrConv.Katakana, 0x411);
        }
    }
}
