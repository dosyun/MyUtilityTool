using System;

namespace MyUtilityTool.Attributes
{
    /// <summary>
    /// 列挙子に表示用などに使用する値を設定するための属性クラスです。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumValueAttribute : Attribute
    {
        /// <summary>
        /// この属性を複数付与したときに一意の値となるためのキーを取得します。
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// この属性を通じて列挙子に付与した値を取得します。
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 列挙子に付与する値を受け取って <see cref="T:MyUtilityTool.Attributes.EnumValueAttribute"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">列挙子に付与する値。</param>
        public EnumValueAttribute(object value) : this("", value) { }

        /// <summary>
        /// この属性が複数付与されたときに一意の値となるためのキーと列挙子に付与する値を受け取って <see cref="T:MyUtilityTool.Attributes.EnumValueAttribute"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="key">この属性が複数付与されたときに一意の値となるためのキー。</param>
        /// <param name="value">列挙子に付与する値。</param>
        public EnumValueAttribute(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
