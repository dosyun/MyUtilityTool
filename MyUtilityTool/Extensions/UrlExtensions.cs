using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyUtilityTool.Extensions
{
    public static class UrlExtensions
    {

        public static NameValueCollection ParseQuery(this Uri uri)
        {
            return uri.ParseQuery(null);
        }

        public static NameValueCollection ParseQuery(this Uri uri, Encoding enc)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            return HttpUtility.ParseQueryString(uri.Query, enc ?? Encoding.UTF8);
        }

        public static Uri AddParameter(this Uri uri, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return new Uri(uriBuilder.ToString());
        }
    }
}
