using System;
using System.Collections.Generic;

namespace PasonatechSystems.Common.Util
{
    /// <summary>nullかも知れない値を表すMaybe
    /// JavaのOptionalを元ねたに、よく使うメソッドだけ実装しました
    /// 便利だからみんな使ってね
    /// なお、LINQと違って即時評価です
    /// </summary>
    /// <typeparam name="T">格納する型</typeparam>
    public interface IMaybe<T> : IEnumerable<T>
    {
        /// <summary>値を取り出す</summary>
        /// <returns>値、ただしNothingに対して使うと例外</returns>
        T Get();

        /// <summary>値を取り出す</summary>
        /// <returns>値、Nothingの場合デフォルト値</returns>
        T OrDefault();

        /// <summary>値があればtrueを返す</summary>
        /// <returns>値があればture</returns>
        bool HasValue { get; }

        /// <summary>値があれば処理を行う</summary>
        /// <param name="consumer">値に対する処理を行うdelegate</param>
        void IfPresent(Action<T> consumer);

        /// <summary>条件に一致すれば自分を、そうでなければNothingを返す</summary>
        /// <param name="predicate">判定式</param>
        /// <returns>条件に一致すれば自身のインスタンス、そうでなければNothing</returns>
        IMaybe<T> Filter(Predicate<T> predicate);

        /// <summary>値を変換してMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        IMaybe<U> Map<U>(Func<T, U> mapper);

        /// <summary>値を変換して引数の式を使ってMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        IMaybe<U> FlatMap<U>(Func<T, IMaybe<U>> mapper);

        /// <summary>値を取り出す。値が無ければデフォルト値（引数）をそのまま返す</summary>
        /// <param name="arg">デフォルト値</param>
        /// <returns>値</returns>
        T OrElse(T arg);

        /// <summary>値を取り出す。値が無ければ引数の式を使ってデフォルト値を生成して返す</summary>
        /// <param name="supplier">デフォルト値を生成するdelegate</param>
        /// <returns>値</returns>
        T OrElseGet(Func<T> supplier);

        /// <summary>値を取り出す。値が無ければ引数の式を使って例外を発生させる</summary>
        /// <typeparam name="X">例外の型</typeparam>
        /// <param name="exceptionSupplier">例外を生成するdelegate</param>
        /// <returns>値</returns>
        T OrElseThrow<X>(Func<X> exceptionSupplier) where X : Exception;

        /// <summary>処理の途中結果をを覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        IMaybe<T> Peek(Action<T> consumer);

        /// <summary>処理の途中で発生した例外を覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        IMaybe<T> PeekException(Action<Exception> consumer);

        /// <summary>Maybeの中で最後に発生した例外を返す</summary>
        /// <returns>発生した例外。ない場合はnull</returns>
        Exception GetException();
    }
}
