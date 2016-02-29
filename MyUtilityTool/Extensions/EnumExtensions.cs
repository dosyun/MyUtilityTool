using System;
using System.Linq;
using MyUtilityTool.Attributes;
using System.Collections.Generic;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.Enum"/> の拡張クラスです。
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// <see cref="T:MyUtilityTool.Attributes.EnumValueAttribute"/> 属性を設定した列挙子から、属性で設定した値を取得します。
        /// </summary>
        /// <typeparam name="T">属性で設定した値の型。</typeparam>
        /// <param name="value">対象の列挙子。</param>
        /// <returns>属性で設定した値。</returns>
        public static T GetAttributeValue<T>(this Enum value)
        {
            return GetAttributeValue<T>(value, "");
        }

        /// <summary>
        /// <see cref="T:MyUtilityTool.Attributes.EnumValueAttribute"/> 属性を設定した列挙子から、属性で設定したキーを元に値を取得します。
        /// </summary>
        /// <typeparam name="T">属性で設定した値の型。</typeparam>
        /// <param name="value">対象の列挙子。</param>
        /// <param name="attributeKey">属性で設定したキー。</param>
        /// <returns>属性で設定した値。</returns>
        public static T GetAttributeValue<T>(this Enum value, string attributeKey)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(EnumValueAttribute), false) as EnumValueAttribute[];
            // AppCode以外のenumでも使われた時のため
            if (attributes == null)
                return default(T);

            var attribute = attributes.FirstOrDefault(x => x.Key == attributeKey);
            if (attributeKey.IsNullOrEmpty() || attribute == null)
                return default(T);

            return (T)attribute.Value;
        }

        /// <summary>
        /// 列挙型に含まれているかどうかを検証します。
        /// </summary>
        /// <typeparam name="T">列挙型の型。</typeparam>
        /// <param name="value">検証する値。</param>
        /// <returns>列挙型に含まれている場合は true。そうでない場合は false。</returns>
        public static bool IsEnum<T>(this object value)
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        /// Enum配列を返却
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Enums<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("指定する型はEnumでないといけません。");

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// valueの値が列挙型に含めれる場合はその値を返す
        /// 含めれない場合は指定したデフォルト値を返す
        /// </summary>
        /// <typeparam name="T">列挙型</typeparam>
        /// <param name="value">比較値</param>
        /// <param name="defaultValue">初期値</param>
        /// <returns>値</returns>
        public static int GetEnumValue<T>(this int value, int defaultValue)
        {
            return value.IsEnum<T>() ? value : defaultValue;
        }

        public static string GetStr(this Enum o, string category)
        {
            return o.GetAttributeValue<string>(category);
        }

        public static int GetInt(this Enum o, string category)
        {
            return o.GetAttributeValue<int>(category);
        }


        /// <summary>
        /// WeightedSampleの復元抽選最大回数
        /// </summary>
        static readonly int WeightedSampleMaxLoopCount = 1000;

        /// <summary>
        /// シーケンスからweightSelectorで指定された重み付けで復元抽選を実施し、当選した要素を返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">抽選対象のシーケンス</param>
        /// <param name="weightSelector">シーケンスの各要素の重みを提供する関数</param>
        /// <returns></returns>
        public static IEnumerable<T> WeightedSample<T>(this IEnumerable<T> source, Func<T, int> weightSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (weightSelector == null) throw new ArgumentNullException("weightSelector");

            return WeightedSampleCore(source, weightSelector, new Random(GenerateRandomSeed()));
        }

        /// <summary>
        /// シーケンスからweightSelectorで指定された重み付けで復元抽選を実施し、当選した要素を返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">抽選対象のシーケンス</param>
        /// <param name="weightSelector">シーケンスの各要素の重みを提供する関数</param>
        /// <param name="random">抽選に使用するランダム生成子</param>
        /// <returns></returns>
        public static IEnumerable<T> WeightedSample<T>(this IEnumerable<T> source, Func<T, int> weightSelector, Random random)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (weightSelector == null) throw new ArgumentNullException("weightSelector");
            if (random == null) throw new ArgumentNullException("random");

            return WeightedSampleCore(source, weightSelector, random);
        }

        static IEnumerable<T> WeightedSampleCore<T>(IEnumerable<T> source, Func<T, int> weightSelector, Random random)
        {
            var totalWeight = 0;
            var sortedArray = source
                .Select(x =>
                {
                    var weight = weightSelector(x);
                    if (weight <= 0) return null;

                    checked { totalWeight += weight; }
                    return new { Value = x, Bound = totalWeight };
                })
                .Where(x => x != null)
                .ToArray();

            if (!sortedArray.Any()) yield break;

            int loopCounter = 0;
            while (true)
            {
                if (++loopCounter > WeightedSampleMaxLoopCount)
                    throw new InvalidOperationException(string.Format("[WeightedSample] 抽選回数が{0}回を超えています。", WeightedSampleMaxLoopCount));

                var draw = random.Next(1, totalWeight + 1);

                var lower = -1;
                var upper = sortedArray.Length;
                while (upper - lower > 1)
                {
                    int index = (lower + upper) / 2;
                    if (sortedArray[index].Bound >= draw)
                    {
                        upper = index;
                    }
                    else
                    {
                        lower = index;
                    }
                }

                yield return sortedArray[upper].Value;
            }
        }

        /// <summary>
        /// ランダム値の作成
        /// </summary>
        /// <returns></returns>
        static int GenerateRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
