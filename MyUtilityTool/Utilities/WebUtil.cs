using MyUtilityTool.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace MyUtilityTool.Utilities
{
    public static class WebUtil
    {        

        #region Url        

        /// <summary>
        /// 不要Keyを除去し ~ 付きUrlを再作成する
        /// </summary>
        internal static string BuildUrl(HttpRequest httpRequest)
        {
            var list = httpRequest.QueryString.Cast<string>()                                   
                                   .Select(key => Tuple.Create(key, httpRequest[key]));

            var url = httpRequest.AppRelativeCurrentExecutionFilePath;
            if (list.Any())
            {
                var query = string.Join("&", list.Select(x => string.Format("{0}={1}", x.Item1, x.Item2)));
                url = string.Format("{0}?{1}", url, query);
            }

            return url;
        }
    
        /// <summary>
        /// QueryStringを除去し、新たなkeyでQueryStringを作成
        /// </summary>
        internal static string BuildUrl(HttpRequest httpRequest, string targetUrl, bool isPager, string[] keys, params object[] values)
        {
            if (keys == null || values == null)
                return BuildUrl(httpRequest);

            int keyCount = keys.Count();
            if (keyCount != values.Count())
                return BuildUrl(httpRequest);            

            var query = string.Empty;
            for (int i = 0; i < keyCount; i++)
            {
                if (0 < i) query += "&";
                query += string.Format("{0}={1}", keys[i], values[i]);
            }

            return targetUrl + "?" + query;
        }

        /// <summary>
        /// 自ページurl作成
        /// </summary>
        internal static string BuildUrl(HttpRequest httpRequest, bool isPager, string[] keys, params object[] values)
        {
            var rawUrl = httpRequest.AppRelativeCurrentExecutionFilePath;
            return BuildUrl(httpRequest, rawUrl, isPager, keys, values);
        }

       

        internal static string GetRequestParameter(HttpRequest req, string key)
        {
            string result = req.Form[key];

            if (string.IsNullOrEmpty(result) && !req.IsQueryStringNull(key))
            {
                result = req.QueryString[key];
            }

            return result;
        }

        /// <summary>
        /// URLとリクエストパラメーターを分離
        /// </summary>
        public static void SplitUrlParam(string baseString, out string url, out string param)
        {
            var splitPoint = baseString.IndexOf("&");
            if (splitPoint > 0)
            {
                url = baseString.Substring(0, splitPoint);
                param = baseString.Substring(splitPoint);
            }
            else
            {
                url = baseString;
                param = string.Empty;
            }
        }

        public static bool IsExistRequestParameter(HttpRequest req, string key)
        {
            var raw = GetRequestParameter(req, key);
            return !string.IsNullOrEmpty(raw);
        }

        public static int GetRequestParameterAsInt(HttpRequest req, string key, int defaultValue = 0)
        {
            var raw = GetRequestParameter(req, key);

            int val;
            if (int.TryParse(raw, out val))
                return val;
            else
                return defaultValue;
        }

        public static T GetRequestParameterAsEnum<T>(this HttpRequest req, string key, T defaultValue)
        {
            var raw = GetRequestParameter(req, key);

            if (string.IsNullOrEmpty(raw))
                return defaultValue;

            try
            {
                return (T)Enum.Parse(typeof(T), raw);
            }
            catch
            {
                return defaultValue;
            }
        }
        #endregion       
        


        #region Control
        /// <summary>
        /// 列挙型をラジオボタンへバインドする
        /// </summary>
        public static void BindEnumDataToRadioButtonList<T>(RadioButtonList radioButtonList, T selectedValue = default(T), int maxBindNum = int.MaxValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("指定された型が列挙型ではありません");

            if (radioButtonList == null)
                throw new ArgumentNullException("dropDownList");

            if (radioButtonList.Items.Count > 0)
                return;

            var items = Enum.GetValues(typeof(T)).Cast<T>()
                .Take(maxBindNum)
                .Select(x =>
                {
                    var label = "";
                    var value = x.ToInt32(CultureInfo.CurrentUICulture.NumberFormat);

                    return new ListItem
                    {
                        Text = label != string.Empty ? label : x.ToString(),
                        Value = value.ToString(),
                        Selected = (x.Equals(selectedValue))
                    };
                })
                .ToArray();

            radioButtonList.Items.AddRange(items);
        }


        /// <summary>
        /// Dictionaryをラジオボタンへバインドする
        /// </summary>
        public static void BindDictionaryToRadioButtonList<T>(RadioButtonList radioButtonList, Dictionary<T, string> items, T selectedValue) where T : struct
        {
            if (radioButtonList == null)
                throw new ArgumentNullException("dropDownList");

            if (radioButtonList.Items.Count > 0)
                return;

            foreach (var item in items)
            {
                radioButtonList.Items.Add(new ListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString(),
                    Selected = item.Key.Equals(selectedValue)
                });
            }
        }


        /// <summary>
        /// Valueのみをラジオボタンへバインドする
        /// </summary>
        public static void BindValueOnlyToRadioButtonList<T>(RadioButtonList radioButtonList, T[] items, T selectedValue) where T : struct
        {
            if (radioButtonList == null)
                throw new ArgumentNullException("dropDownList");

            if (radioButtonList.Items.Count > 0)
                return;

            foreach (var item in items)
            {
                radioButtonList.Items.Add(new ListItem
                {
                    Text = "",
                    Value = item.ToString(),
                    Selected = item.Equals(selectedValue)
                });
            }
        }
        #endregion


        #region JavaScript
        /// <summary>
        /// 引数のオブジェクトのJson文字列を取得する。
        /// </summary>
        /// <param name="obj">対象オブジェクト</param>
        /// <returns>Json文字列</returns>
        public static string GetJsonString(object obj)
        {
            if (obj == null)
                return string.Empty;

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static object GetJsonObject(string str, Type targetType)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize(str, targetType);

        }
        #endregion


    }
}
