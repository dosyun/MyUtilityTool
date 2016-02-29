using MyUtilityTool.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MyUtilityTool.Utilities
{
    public class StringUtil
    {

        private const string BASECHAR = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static DateTime StringToDateTime(string s)
        {

            string format = "yyyy/M/d";
            DateTime rDate = new DateTime();
            if (!DateTime.TryParseExact(s, format, null, DateTimeStyles.None, out rDate))
            {
                format = "yyyy/M/d HH:mm:ss";
                if (!DateTime.TryParseExact(s, format, null, DateTimeStyles.None, out rDate))
                    throw new ValidationException("value", s);
            }

            return rDate;
        }

        public static DateTime StringToDateTime(string s, string format)
        {
            DateTime rDate;
            if (!DateTime.TryParseExact(s, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out rDate))
                throw new ValidationException("value", s);
            return rDate;
        }

        public static bool StringToBool(string s)
        {
            if (s == "0")
                return false;
            else if (s == "1")
                return true;
            else
                throw new ValidationException("value", s);
        }

        public static int StringToInt(string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch
            {
                throw new ValidationException("value", s);
            }
        }

        public static long StringToLong(string s)
        {
            try
            {
                return long.Parse(s);
            }
            catch
            {
                throw new ValidationException("value", s);
            }
        }

        public static string[] DivideCommaSeparatedString(string s)
        {
            return SplitUniq(s, new char[] { ',' });
        }

        public static string[] DivideSpaceSeparatedString(string s)
        {
            return SplitUniq(s, new char[] { ' ', '　' });
        }

        public static string[] SplitUniq(string input, char[] separators)
        {
            return ArrayUtil.Uniq(
                Array.FindAll(
                    Array.ConvertAll<string, string>(
                        input.Split(separators),
                        delegate (string s) { return s.Trim(); }
                    ),
                    delegate (string s) { return !string.IsNullOrEmpty(s); }
                )
            );
        }

        public static string GenerateRandomString(int len)
        {
            return GenerateRandomString(len, BASECHAR);
        }

        public static string GenerateRandomString(int len, string baseChars)
        {
            Random random = RandomUtil.UniqueRandom;
            int iSrcLen = baseChars.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                int idx = random.Next(0, iSrcLen - 1);
                sb.Append(baseChars[idx]);
            }
            return sb.ToString();
        }

        private static Regex PATTERN_UNICODE = new Regex(@"%(u[0-9a-fA-F]{4})");
        private static Regex PATTERN_HTML_TAG = new Regex(@"<.*?>", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex PATTERN_WIKI_TAG = new Regex(@"\[(image|youtube|amazon|nicovideo):.*?\]", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex PATTERN_NEWLINE = new Regex(@"[\n\s]+");

        public static string UnicodeEscape(string value)
        {
            return HttpUtility.UrlDecode(PATTERN_UNICODE.Replace(HttpUtility.UrlEncodeUnicode(value), @"\$1"));
        }

        public static string RemoveHtmlTag(string p)
        {
            return PATTERN_HTML_TAG.Replace(p, "");
        }

        public static string RemoveWikiTag(string p)
        {
            return PATTERN_WIKI_TAG.Replace(p, "");
        }

        public static string ParseUrl(string input)
        {
            return Regex.Replace(input, @"((href|src|id)=[""']?)?(https?|ftp)(:\/\/[-_.!~*\'()a-zA-Z0-9;\/?:\@&=+\$,%#]+)", delegate (Match m)
            {
                string match = m.Value;
                if (match.StartsWith("href=") || match.StartsWith("src=") || match.StartsWith("id="))
                {
                    return match;
                }
                else
                {
                    return string.Format(@"<a href=""{0}"" target=""_blank"">{0}</a>", match);
                }
            });
        }

        public static bool IsValidUrl(string input)
        {
            string pat = @"(https?|ftp)(:\/\/[-_.!~*\'()a-zA-Z0-9;\/?:\@&=+\$,%#]+)$";
            return Regex.IsMatch(input, pat, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        /// <summary>
        /// 指定文字数でカット
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Trim(string input, int len)
        {
            if (input == null)
                return null;
            else
                return input.Length > len ? input.Substring(0, len) : input;
        }

        /// <summary>
        /// サマリ文字列生成処理（絵文字[e:XXX]を１文字としてカウント）
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static string GenerateSummary(string from, int cnt, string end)
        {
            string rtn = from;

            //0. 改行コードと空白文字(の連続)を半角スペースに置換
            rtn = rtn.Replace("&nbsp;", " ");
            if (rtn.Contains("\r\n") || rtn.Contains("\r") || rtn.Contains("\n"))
            {
                rtn = rtn.Replace("\r\n", "\n");
                rtn = rtn.Replace("\r", "\n");

                rtn = PATTERN_NEWLINE.Replace(rtn, " ");
            }


            //1. #を\#にエスケープ
            rtn = rtn.Replace("#", "\\#");

            //2. [e:XXX]を#に置換
            Queue<string> emojis = new Queue<string>();
            rtn = Regex.Replace(rtn, @"\[e:(?<emoji>\d+)\]", delegate (Match m)
            {
                emojis.Enqueue(string.Format("[e:{0}]", m.Result("${emoji}")));
                return "#";
            });

            //3. 規定の文字列長になるまで末尾から文字を除いていく
            bool isTrim = false;
            string tmp = rtn;
            int i = tmp.Length;
            while (tmp.Replace("\\#", "#").Length > cnt)
            {
                tmp = rtn.Substring(0, i);
                if (tmp.EndsWith("\\") && rtn[i] == '#') tmp = rtn.Substring(0, i - 1); //"\#"はまとめて削除
                i--;
                isTrim = true;
            }
            rtn = tmp;

            //4. #を[e:XXX]に戻す
            rtn = Regex.Replace(rtn, @"(?<!\\)#", delegate (Match m)
            {
                return emojis.Dequeue();
            });

            //5. \#を#に戻す
            rtn = rtn.Replace("\\#", "#");

            return (isTrim) ? rtn + end : rtn;
        }

        public static string EncodeForHtml(string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\r", "\n");
            text = text.Replace("\n", "<br />");

            return text;
        }

        public static string RemoveTags(string text)
        {
            text = RemoveWikiTag(text);
            text = RemoveHtmlTag(text);

            return text;
        }


        /// <summary>
        /// String[]をまとめてIsNullOrEmptyチェックする
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static bool HasEmptyStrings(params string[] inputs)
        {
            foreach (string str in inputs)
                if (string.IsNullOrEmpty(str)) return true;
            return false;
        }

        static string zenhan_table_w = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン゛゜ァィゥェォャュョッＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ１２３４５６７８９！＃＄％＆’（）＋－＝＠［］＾＿‘｛｝～。「」『』、・ー”";
        static string zenhan_table_n = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝﾞﾟｧｨｩｪｫｬｭｮｯABCDEFGHIJKLMNOPQRSTUVWXYZ123456789!#$%&'()+-=@[]^_`{}~｡｢｣｢｣､･-\"";
        static string dakuon_table_w = "ガギグゲゴザジズゼゾダヂヅデドバビブベボ";
        static string dakuon_table_n = "カキクケコサシスセソタチツテトハヒフヘホ";
        static string handaku_table_w = "パピプペポ";
        static string handaku_table_n = "ハヒフヘホ";
        /// <summary>
        /// 全角カナ英数の半角文字変換処理
        /// </summary>
        /// <param name="name">変換する文字列</param>
        /// <returns>変換された文字列</returns>
        public static string Zen2Han(string name)
        {
            for (int i = 0; i < dakuon_table_n.Length; i++)
            {
                name = name.Replace(dakuon_table_w[i].ToString(), dakuon_table_n[i] + "゛");
            }
            for (int i = 0; i < handaku_table_n.Length; i++)
            {
                name = name.Replace(handaku_table_w[i].ToString(), handaku_table_n[i] + "゜");
            }
            for (int i = 0; i < zenhan_table_n.Length; i++)
            {
                name = name.Replace(zenhan_table_w[i], zenhan_table_n[i]);
            }
            name = name.Replace(" ", "");
            return name;
        }

        /// <summary>
        /// SHA1ハッシュ値を計算する
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string ComputeSHA1Hash(string value, Encoding enc)
        {
            //文字列をバイト型配列に変換する
            byte[] data = enc.GetBytes(value);
            //ハッシュ値を計算
            byte[] bs = SHA1Managed.Create().ComputeHash(data);
            //byte型配列を16進数の文字列に変換
            return BitConverter.ToString(bs).ToLower().Replace("-", "");
        }

        /// <summary>
        /// MD5ハッシュ値を計算する
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string ComputeMD5Hash(string value, Encoding enc)
        {
            //文字列をバイト型配列に変換する
            byte[] data = enc.GetBytes(value);
            //ハッシュ値を計算
            byte[] bs = MD5.Create().ComputeHash(data);
            //byte型配列を16進数の文字列に変換
            return BitConverter.ToString(bs).ToLower().Replace("-", "");
        }

        public static string ComputeSaltedHash(string value, string salt)
        {
            return ComputeSaltedHash(value, salt, Encoding.UTF8);
        }

        public static string ComputeSaltedHash(string value, string salt, Encoding enc)
        {
            return ComputeSaltedHash(value, salt, "SHA1", enc);
        }

        //hashAlgorithmを変えたい要件はとりあえずなさそうなのでprivateにしておく
        public static string ComputeSaltedHash(string value, string salt, string hashAlgorithm, Encoding enc)
        {
            switch (hashAlgorithm)
            {
                case "MD5":
                    return ComputeMD5Hash(value + salt, enc);
                case "SHA1":
                default:
                    return ComputeSHA1Hash(value + salt, enc);
            }
        }
      

        public static int ByteLength(string s)
        {
            return Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("Shift_JIS"), Encoding.UTF8.GetBytes(s)).Length;
        }

        public static string PadRight(string s, int len)
        {
            int pad = len - ByteLength(s);
            if (pad <= 0) return s;
            StringBuilder sb = new StringBuilder(s);
            Enumerable.Range(0, pad).ToList().ForEach(x => sb.Append(" "));
            return sb.ToString();
        }

        public static string PadLeft(string s, int len)
        {
            int pad = len - ByteLength(s);
            if (pad <= 0) return s;
            StringBuilder sb = new StringBuilder();
            Enumerable.Range(0, pad).ToList().ForEach(x => sb.Append(" "));
            return sb.Append(s).ToString();
        }

        public static bool HasString(string text, string target)
        {
            if (text == "")
                return false;
            if (target.IndexOf(text) >= 0)
                return true;
            else
                return false;
        }

        private static readonly char TABLE_SEPARATOR = '|';

        public static void Prettify(Action<string> output, string[] headers, List<string[]> data_rows)
        {
            Dictionary<int, int> column_length = new Dictionary<int, int>();

            List<string[]> rows = new List<string[]>();
            rows.Add(headers);
            rows.AddRange(data_rows);

            foreach (string[] line in rows)
            {
                int[] lens = line.Select(s => ByteLength(s)).ToArray();
                for (int i = 0; i < lens.Length; i++)
                {
                    if (!column_length.ContainsKey(i))
                        column_length[i] = 0;
                    column_length[i] = Math.Max(lens[i], column_length[i]);
                }
            }

            string line_separator = string.Join("", Enumerable.Repeat("-", column_length.Sum(kv => kv.Value) + column_length.Count + 1).ToArray());

            //出力開始
            output(line_separator);

            for (int i = 0; i < rows.Count; i++)
            {
                string[] cols = rows[i];
                StringBuilder sb = new StringBuilder(TABLE_SEPARATOR.ToString());
                for (int j = 0; j < cols.Length; j++)
                {
                    double d;
                    if (double.TryParse(cols[j], out d))
                        sb.Append(PadLeft(cols[j], column_length[j])).Append(TABLE_SEPARATOR);
                    else
                        sb.Append(PadRight(cols[j], column_length[j])).Append(TABLE_SEPARATOR);
                }
                output(sb.ToString());

                if (i == 0)
                    output(line_separator);
            }
            output(line_separator);
        }

        /// <summary>
        /// 機種依存文字をエンティティ参照に変換する
        /// 
        /// 文字の範囲は以下のサイトを参考にした
        /// http://www.d-toybox.com/studio/lib/romanNumerals.html#sample
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertPlatformDependentCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder();

            foreach (char c in text.ToArray())
            {
                int i = (int)c;

                if ((0x2150 <= i && i <= 0x219F)
                 || (0x2460 <= i && i <= 0x24EF)
                 || (0x2600 <= i && i <= 0x2660)
                 || (0x3220 <= i && i <= 0x324F)
                 || (0x3280 <= i && i <= 0x33FF)
                 || i == 0xFF5E)
                {
                    sb.AppendFormat("&#{0};", i);
                }
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }


        /// <summary>
        /// Unicode文字列でShift_JIS以外の文字を?に変換する
        /// 参考: http://blog.livedoor.jp/pandora200x/archives/50087762.html
        /// </summary>
        public static string ExceptCharactorsOtherThanShiftJIS(string unicodeStrings)
        {
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeByte = unicode.GetBytes(unicodeStrings);
            Encoding s_jis = Encoding.GetEncoding("shift_jis");

            //shift_jisに変換
            byte[] s_jisByte = Encoding.Convert(unicode, s_jis, unicodeByte);
            //unicodeに変換
            byte[] newUnicodeByte = Encoding.Convert(s_jis, unicode, s_jisByte);

            char[] unicodeChars = new char[unicode.GetCharCount(newUnicodeByte, 0, newUnicodeByte.Length)];
            unicode.GetChars(newUnicodeByte, 0, newUnicodeByte.Length, unicodeChars, 0);
            return new string(unicodeChars);
        }
    }
}
