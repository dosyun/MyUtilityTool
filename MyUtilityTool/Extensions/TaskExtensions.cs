using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilityTool.Extensions
{
    /// <summary>
    /// <see cref="T:System.Threading.Tasks.Task"/> の拡張クラスです。
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// 指定されたすべてのタスクが完了してから完了するタスクを生成します。
        /// </summary>
        /// <param name="tasks">完了を待機するタスク。</param>
        /// <returns>指定されたすべてのタスクの完了を表すタスク。</returns>
        public static Task WhenAll(this IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 指定されたすべてのタスクが完了してから完了するタスクを生成します。
        /// </summary>
        /// <typeparam name="T">完了したタスクの型。</typeparam>
        /// <param name="tasks">完了を待機するタスク。</param>
        /// <returns>指定されたすべてのタスクの完了を表すタスク。</returns>
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 指定されたタスクを投げっぱなしにし、メインスレッドへの行流抑制とコンパイラの警告抑制を行います。
        /// </summary>
        /// <param name="task">投げっぱなしにするタスク。</param>
        /// <param name="errorAction">例外が発生したときに実行する関数。</param>
        public static void FireAndForget(this Task task, Action<Task> errorAction)
        {
            task.ConfigureAwait(false);
            task.ContinueWith(errorAction, TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// 指定されたタスクを投げっぱなしにし、メインスレッドへの行流抑制とコンパイラの警告抑制を行います。
        /// </summary>
        /// <typeparam name="T">タスクが完了したときの型。</typeparam>
        /// <param name="task">投げっぱなしにするタスク。</param>
        /// <param name="errorAction">例外が発生したときに実行する関数。</param>
        public static void FireAndForget<T>(this Task<T> task, Action<Task<T>> errorAction)
        {
            task.ConfigureAwait(false);
            task.ContinueWith(errorAction, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
