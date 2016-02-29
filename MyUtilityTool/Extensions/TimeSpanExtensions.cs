using System;

namespace MyUtilityTool.Extention
{

    public static class RogueTimeSpanEx
    {
        /// <summary>
        /// 期間を表す文字列を返す
        /// </summary>
        public static string ToTimeSpanString(this TimeSpan span)
        {
            if (span.TotalSeconds <= 0)
                return "";

            if (span.TotalMinutes < 1)
                return string.Format("{0}秒", span.Seconds);

            if (span.TotalHours < 1)
                return string.Format("{0}分{1}", span.Minutes, span.Seconds > 0 ? string.Format("{0}秒", span.Seconds) : "");

            if (span.TotalDays < 1)
                return string.Format("{0}時間{1}", span.Hours, span.Minutes > 0 ? string.Format("{0}分", span.Minutes) : "");

            return string.Format("{0}日{1}", span.Days, span.Hours > 0 ? string.Format("と{0}時間", span.Hours) : "");
        }

        /// <summary>
        /// 時間間隔が相当する年を取得します。
        /// </summary>
        public static int Years(this TimeSpan span)
        {
            var baseDate = span.Ticks > 0
                ? DateTime.MinValue
                : DateTime.MaxValue;

            return (baseDate + span).Year - baseDate.Year;
        }

        /// <summary>
        /// 文字列変換用のDateTimeを返します
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this TimeSpan span)
        {
            return new DateTime(0).Add(span);
        }
    }

}


