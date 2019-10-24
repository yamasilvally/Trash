using System;
using System.Collections.Generic;

namespace PasonatechSystems.Common.Util
{
    /// <summary>Maybeのファクトリ</summary>
    public static class Maybe
    {
        /// <summary>空のMaybeを返す</summary>
        /// <typeparam name="T">格納する型</typeparam>
        /// <returns>Nothing</returns>
        public static IMaybe<T> Empty<T>()
        {
            return new Nothing<T>();
        }

        /// <summary>Just<typeparamref name=">T"/>を返す、引数がnullなら空のMaybeを返す</summary>
        /// <typeparam name="T">引数の型</typeparam>
        /// <param name="arg">任意のオブジェクト</param>
        /// <returns>Just</returns>
        public static IMaybe<T> Of<T>(T arg, bool lazy = false)
        {
            if (arg == null)
            {
                return lazy ? OfLazy(arg) : new Nothing<T>();
            }

            return lazy ? OfLazy(arg) : new Just<T>(arg);
        }

        /// <summary>遅延評価型Just<typeparamref name=">T"/>を返す、引数がnullなら空のMaybeを返す</summary>
        /// <typeparam name="T">引数の型</typeparam>
        /// <param name="arg">任意のオブジェクト</param>
        /// <returns>Just</returns>
        public static IMaybe<T> OfLazy<T>(T arg)
        {
            if (arg == null)
            {
                return new LazyMaybe<T>(() => new Nothing<T>());
            }

            return new LazyMaybe<T>(() => new Just<T>(arg));
        }
    }
}
