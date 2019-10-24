using System;
using System.Collections;
using System.Collections.Generic;

namespace PasonatechSystems.Common.Util
{

    /// <summary>Nothing</summary>
    /// <typeparam name="T">格納するはずだった値の型</typeparam>
    public sealed class Nothing<T> : IMaybe<T>
    {
        private T[] ary = new T[]{ };

        private Exception exception;

        #region [ constructors ]

        /// <summary>constructor</summary>
        public Nothing()
        {
        }

        public Nothing(Exception e)
        {
            this.exception = e;
        }

        #endregion

        #region [ properties ]

        /// <summary>値があればtrueを返す</summary>
        public bool HasValue
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region [ methods ]

        /// <summary>値を取り出す</summary>
        /// <returns>値、ただしNothingに対して使うと例外</returns>
        public T Get()
        {
            throw new InvalidOperationException("no value");
        }

        /// <summary>値を取り出す</summary>
        /// <returns>値、Nothingの場合デフォルト値</returns>
        public T OrDefault()
        {
            return default(T);
        }

        /// <summary>値があれば処理を行う</summary>
        /// <param name="consumer">値に対する処理を行うdelegate</param>
        public void IfPresent(Action<T> consumer)
        {
            // no operation
        }

        /// <summary>条件に一致すれば自分を、そうでなければNothingを返す</summary>
        /// <param name="predicate">判定式</param>
        /// <returns>条件に一致すれば自身のインスタンス、そうでなければNothing</returns>
        public IMaybe<T> Filter(Predicate<T> predicate)
        {
            return this;
        }

        /// <summary>値を変換してMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        public IMaybe<U> Map<U>(Func<T, U> mapper)
        {
            return new Nothing<U>();
        }

        /// <summary>値を変換して引数の式を使ってMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        public IMaybe<U> FlatMap<U>(Func<T, IMaybe<U>> mapper)
        {
            return new Nothing<U>();
        }

        /// <summary>値を取り出す。値が無ければデフォルト値（引数）をそのまま返す</summary>
        /// <param name="arg">デフォルト値</param>
        /// <returns>値</returns>
        public T OrElse(T arg)
        {
            return arg;
        }

        /// <summary>値を取り出す。値が無ければ引数の式を使ってデフォルト値を生成して返す</summary>
        /// <param name="supplier">デフォルト値を生成するdelegate</param>
        /// <returns>値</returns>
        public T OrElseGet(Func<T> supplier)
        {
            return supplier();
        }

        /// <summary>値を取り出す。値が無ければ引数の式を使って例外を発生させる</summary>
        /// <typeparam name="X">例外の型</typeparam>
        /// <param name="exceptionSupplier">例外を生成するdelegate</param>
        /// <returns>値</returns>
        public T OrElseThrow<X>(Func<X> exceptionSupplier) where X : Exception
        {
            throw exceptionSupplier();
        }

        /// <summary>処理の途中結果をを覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        public IMaybe<T> Peek(Action<T> consumer)
        {
            try
            {
                consumer(default(T));
            }
            catch (Exception e)
            {
                this.exception = e;
            }

            return this;
        }

        /// <summary>処理の途中で発生した例外を覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        public IMaybe<T> PeekException(Action<Exception> consumer)
        {
            try
            {
                consumer(this.exception);
            }
            catch (Exception e)
            {
                this.exception = e;
            }

            return this;
        }

        /// <summary>Maybeの中で最後に発生した例外を返す</summary>
        /// <returns>発生した例外。ない場合はnull</returns>
        public Exception GetException()
        {
            return this.exception;
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>
        /// コレクションを反復処理するために使用できる <see cref="T:System.Collections.Generic.IEnumerator`1" />。
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var i in this.ary)
            {
                yield return i;
            }
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
