using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace MyUtilityTool.Attributes
{
    /// <summary>
    /// アプリケーション起動時にコンストラクターを実行するための属性クラスです。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PreloadAttribute : Attribute
    {
    }

    /// <summary>
    /// <see cref="T: MyUtilityTool.Attributes.PreloadAttribute"/> 属性の付いたクラスを操作する汎用クラスです。
    /// </summary>
    public class PreloadUtility
    {
        /// <summary>
        /// <see cref="T: MyUtilityTool.Attributes.PreloadAttribute"/> 属性の付いたクラスの既定のコンストラクタを実行します。
        /// </summary>
        /// <param name="errorHandler">例外が発生した時の処理。</param>
        public static void Initialize(Action<ConcurrentDictionary<string, Exception>> errorHandler)
        {
            var exceptions = new ConcurrentDictionary<string, Exception>();

            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetCustomAttributes(typeof(PreloadAttribute)).Any())
                .AsParallel()
                .ForAll(type =>
                {
                    try
                    {
                        var constructor = type.TypeInitializer;
                        constructor?.Invoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        exceptions.TryAdd(type.Name, ex);
                    }
                });

            if (!exceptions.Any())
                return;

            errorHandler(exceptions);
        }
    }
}
