using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilityTool.Validation
{
    public class ValidationException : ApplicationException
    {
        /// <summary>
        /// 指定したパラメータ値が不正
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public ValidationException(string name, object value)
            : base(string.Format("invalid parameter/{0}={1}", name, value))
        {
        }

        /// <summary>
        /// 指定したパラメータ値が不正
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        public ValidationException(string name, object value, object expected)
            : base(string.Format("invalid parameter/{0}={1}(expected:{2})", name, value, expected))
        {
        }
    }
}
