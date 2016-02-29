using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyUtilityTool.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsQueryStringNull(this HttpRequest request, string key)
        {
            return string.IsNullOrEmpty(request.QueryString[key]);
        }

        public static int QueryStringAsInt(this HttpRequest request, string key)
        {
            return int.Parse(request.QueryString[key]);
        }

        public static int QueryStringAsInt(this HttpRequest request, string key, int defaultValue)
        {
            int i;
            if (int.TryParse(request.QueryString[key], out i))
                return i;
            else
                return defaultValue;
        }

        public static long QueryStringAsLong(this HttpRequest request, string key)
        {
            return long.Parse(request.QueryString[key]);
        }

        public static long QueryStringAsLong(this HttpRequest request, string key, long defaultValue)
        {
            long l;
            if (long.TryParse(request.QueryString[key], out l))
                return l;
            else
                return defaultValue;
        }

        public static T QueryStringAsEnum<T>(this HttpRequest request, string key)
        {
            return (T)Enum.Parse(typeof(T), request.QueryString[key]);
        }

        public static T QueryStringAsEnum<T>(this HttpRequest request, string key, T defaultValue)
        {
            if (request.IsQueryStringNull(key))
                return defaultValue;

            try
            {
                return (T)Enum.Parse(typeof(T), request.QueryString[key]);
            }
            catch
            {
                return defaultValue;
            }
        }    

        public static bool IsFormNull(this HttpRequest request, string key)
        {
            return string.IsNullOrEmpty(request.Form[key]);
        }

        public static int FormAsInt(this HttpRequest request, string key)
        {
            return int.Parse(request.Form[key]);
        }

        public static int FormAsInt(this HttpRequest request, string key, int defaultValue)
        {
            int i;
            if (int.TryParse(request.Form[key], out i))
                return i;
            else
                return defaultValue;
        }

        public static long FormAsLong(this HttpRequest request, string key)
        {
            return long.Parse(request.Form[key]);
        }

        public static long FormAsLong(this HttpRequest request, string key, long defaultValue)
        {
            long l;
            if (long.TryParse(request.Form[key], out l))
                return l;
            else
                return defaultValue;
        }

        public static T FormAsEnum<T>(this HttpRequest request, string key)
        {
            return (T)Enum.Parse(typeof(T), request.Form[key]);
        }

        public static T FormAsEnum<T>(this HttpRequest request, string key, T defaultValue)
        {
            if (request.IsFormNull(key))
                return defaultValue;

            try
            {
                return (T)Enum.Parse(typeof(T), request.Form[key]);
            }
            catch
            {
                return defaultValue;
            }
        }   

        //RawUrlをUri形式で返却
        public static Uri GetRawUrl(this HttpRequest request)
        {
            return new Uri(request.Url.GetLeftPart(UriPartial.Authority) + request.RawUrl);
        }
    }
}
