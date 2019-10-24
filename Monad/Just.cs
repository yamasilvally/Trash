using System;
using System.Collections;
using System.Collections.Generic;

namespace PasonatechSystems.Common.Util
{
    /// <summary>Just</summary>
    /// <typeparam name="T">格納する値の型</typeparam>
    public sealed class Just<T> : IMaybe<T>
    {
        #region [ fields ]

        /// <summary>値</summary>
        private T val;

        /// <summary>値の配列</summary>
        private T[] ary;

        #endregion

        #region [ constructors ]

        /// <summary>constructor</summary>
        /// <param name="arg">値</param>
        public Just(T arg)
        {
            this.val = arg;
            this.ary = new T[] { arg };
        }

        /// <summary>constructor</summary>
        private Just()
        {
        }

        #endregion

        #region [ properties ]

        /// <summary>値があればtrueを返す</summary>
        public bool HasValue
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region [ methods ]

        /// <summary>値を取り出す</summary>
        /// <returns>値、ただしNothingに対して使うと例外</returns>
        public T Get()
        {
            return this.val;
        }

        /// <summary>値を取り出す</summary>
        /// <returns>値、Nothingの場合デフォルト値</returns>
        public T OrDefault()
        {
            return this.val;
        }

        /// <summary>値があれば処理を行う</summary>
        /// <param name="consumer">値に対する処理を行うdelegate</param>
        public void IfPresent(Action<T> consumer)
        {
            consumer(this.val);
        }

        /// <summary>条件に一致すれば自分を、そうでなければNothingを返す</summary>
        /// <param name="predicate">判定式</param>
        /// <returns>条件に一致すれば自身のインスタンス、そうでなければNothing</returns>
        public IMaybe<T> Filter(Predicate<T> predicate)
        {
            if (predicate(this.val))
            {
                return this;
            }

            return new Nothing<T>();
        }

        /// <summary>値を変換してMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        public IMaybe<U> Map<U>(Func<T, U> mapper)
        {
            try
            {
                U result = mapper(this.val);
                if (result != null)
                {
                    return new Just<U>(result);
                }
                else
                {
                    return new Nothing<U>();
                }
            }
            catch (Exception e)
            {
                return new Nothing<U>(e);
            }
        }

        /// <summary>値を変換して引数の式を使ってMaybeにして返す</summary>
        /// <typeparam name="U">変換後の値の型</typeparam>
        /// <param name="mapper">変換処理を行うdelegate</param>
        /// <returns>変換後の値を格納したMaybe</returns>
        public IMaybe<U> FlatMap<U>(Func<T, IMaybe<U>> mapper)
        {
            try
            {
                return mapper(this.val);
            }
            catch (Exception e)
            {
                return new Nothing<U>(e);
            }
        }

        /// <summary>値を取り出す。値が無ければデフォルト値（引数）をそのまま返す</summary>
        /// <param name="arg">デフォルト値</param>
        /// <returns>値</returns>
        public T OrElse(T arg)
        {
            return this.val;
        }

        /// <summary>値を取り出す。値が無ければ引数の式を使ってデフォルト値を生成して返す</summary>
        /// <param name="supplier">デフォルト値を生成するdelegate</param>
        /// <returns>値</returns>
        public T OrElseGet(Func<T> supplier)
        {
            return this.val;
        }

        /// <summary>値を取り出す。値が無ければ引数の式を使って例外を発生させる</summary>
        /// <typeparam name="X">例外の型</typeparam>
        /// <param name="exceptionSupplier">例外を生成する式</param>
        /// <returns>例外</returns>
        public T OrElseThrow<X>(Func<X> exceptionSupplier) where X : Exception
        {
            return this.val;
        }

        /// <summary>処理の途中結果を覗く</summary>
        /// <param name="consumer">取り出し先のdelegate</param>
        /// <returns>自身のインスタンス</returns>
        public IMaybe<T> Peek(Action<T> consumer)
        {
            try
            {
                consumer(this.val);
            }
            catch (Exception e)
            {
                return new Nothing<T>(e);
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
                consumer(null);
            }
            catch (Exception e)
            {
                return new Nothing<T>(e);
            }

            return this;
        }

        /// <summary>Maybeの中で最後に発生した例外を返す</summary>
        /// <returns>発生した例外。ない場合はnull</returns>
        public Exception GetException()
        {
            return null;
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
