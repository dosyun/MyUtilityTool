using MyUtilityTool.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyUtilityTool.View
{

    public abstract class AbstractViewModel<T>
    {
        private string _buildUrl;

        internal T User { get; private set; }
        protected HttpRequest Request { get; private set; }
        protected bool IsSmart { get; private set; }
        internal virtual bool CanView { get { return true; } }

        internal AbstractViewModel(T user, HttpRequest req, bool isSmart)
        {
            User = user;
            Request = req;
            IsSmart = isSmart;
        }

        internal static TR GetInstance<TR, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            var type = typeof(TR);
            var ctor = type.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[] { typeof(T1), typeof(T2), typeof(T3) },
                null);

            if (ctor == null)
                throw new NotSupportedException("コンストラクタが定義されていません。");

            return (TR)ctor.Invoke(new object[] { arg1, arg2, arg3 });
        }

        protected string BuildUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_buildUrl))
                    _buildUrl = WebUtil.BuildUrl(Request);
                return _buildUrl;
            }
        }

        protected string BuildTargetUrl(string targetUrl, string[] keys, params object[] values)
        {
            return WebUtil.BuildUrl(Request, targetUrl, false, keys, values);
        }

        protected string BuildPagerUrl(string[] keys, params object[] values)
        {
            return WebUtil.BuildUrl(Request, true, keys, values);
        }
       
    }

}
