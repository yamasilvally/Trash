using System;
using System.Collections;
using System.Collections.Generic;

namespace PasonatechSystems.Common.Util
{
    /// <summary>遅延評価型Maybe</summary>
    /// <typeparam name="T">格納する値の型</typeparam>
    public sealed class LazyMaybe<T> : IMaybe<T>
    {
        #region [ fields ]

        /// <summary>値</summary>
        private Func<IMaybe<T>> func;

        #endregion

        #region [ constructors ]

        /// <summary>constructor</summary>
        /// <param name="func">IMaybeを返すdelegate</param>
        public LazyMaybe(Func<IMaybe<T>> func)
        {
            this.func = func;
        }

        /// <summary>constructor</summary>
        private LazyMaybe()
        {
        }

        #endregion

        #region [ properties ]

        /// <summary>値があればtrueを返す</summary>
        public bool HasValue
        {
            get
            {
                return this.func().HasValue;
            }
        }

        #endregion

        #region [ methods ]

        /// <summary>値を取り出す</summary>
        /// <returns>値、ただしNothingに対して使うと例外</returns>
        public T Get()
        {
            return this.func().Get();
        }

        /// <summary>値を取り出す</summary>
        /// <returns>値、Nothingの場合デフォルト値</returns>
        public T OrDefault()
        {
            return this.func().OrDefault();
        }

        /// <summary>値があれば処理を行う</summary>
        /// <param name="consumer">値に対する処理を行うdelegate</param>
        public void IfPresent(Action<T> consumer)
        {
            this.func().IfPresent(consumer);
        }

        /// <summary>条件に一致すれば自分を、そうでなければNothingを返す</summary>
        /// <param name="predicate">判定式</param>
        /// <returns>条件に一致すれば自身のインスタンス、そうでなければNothing</returns>
        public IMaybe<T> Filter(Predicate<T> predicate)
        {
            return new LazyMaybe<T>(() => this.func().Filter(predicate));
        }

        /// <summary>値を変換してMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        public IMaybe<U> Map<U>(Func<T, U> mapper)
        {
            return new LazyMaybe<U>(() => this.func().Map(mapper));
        }

        /// <summary>値を変換して引数の式を使ってMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        public IMaybe<U> FlatMap<U>(Func<T, IMaybe<U>> mapper)
        {
            return new LazyMaybe<U>(() => this.func().FlatMap(mapper));
        }

        /// <summary>値を取り出す。値が無ければデフォルト値（引数）をそのまま返す</summary>
        /// <param name="arg">デフォルト値</param>
        /// <returns>値</returns>
        public T OrElse(T arg)
        {
            return this.func().OrElse(arg);
        }

        /// <summary>値を取り出す。値が無ければ引数の式を使ってデフォルト値を生成して返す</summary>
        /// <param name="supplier">デフォルト値を生成するdelegate</param>
        /// <returns>値</returns>
        public T OrElseGet(Func<T> supplier)
        {
            return this.func().OrElseGet(supplier);
        }

        /// <summary>値を取り出す。値が無ければ引数の式を使って例外を発生させる</summary>
        /// <typeparam name="X">例外の型</typeparam>
        /// <param name="exceptionSupplier">例外を生成する式</param>
        /// <returns>例外</returns>
        public T OrElseThrow<X>(Func<X> exceptionSupplier) where X : Exception
        {
            return this.func().OrElseThrow(exceptionSupplier);
        }

        /// <summary>処理の途中結果を覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        public IMaybe<T> Peek(Action<T> consumer)
        {
            return new LazyMaybe<T>(() => this.func().Peek(consumer));
        }

        /// <summary>処理の途中で発生した例外を覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        public IMaybe<T> PeekException(Action<Exception> consumer)
        {
            return new LazyMaybe<T>(() => this.func().PeekException(consumer));
        }

        /// <summary>Maybeの中で最後に発生した例外を返す</summary>
        /// <returns>発生した例外。ない場合はnull</returns>
        public Exception GetException()
        {
            return this.func().GetException();
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>
        /// コレクションを反復処理するために使用できる <see cref="T:System.Collections.Generic.IEnumerator`1" />。
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.func().GetEnumerator();
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>
        /// コレクションを反復処理するために使用できる <see cref="T:System.Collections.IEnumerator" /> オブジェクト。
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
