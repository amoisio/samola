using System;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Linq.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOuter"></typeparam>
        /// <typeparam name="TInner"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="outer"></param>
        /// <param name="inner"></param>
        /// <param name="outerKeySelector"></param>
        /// <param name="innerKeySelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            if (!typeof(TKey).IsPrimitive && typeof(TKey) != typeof(string))
                throw new ArgumentException($"{nameof(TKey)} must be of primitive or string type.");

            if (inner == null)
                throw new ArgumentNullException(nameof(inner), "inner cannot be null");

            var outerIndex = outer.GroupBy(outerKeySelector, o => o).ToDictionary(g => g.Key);
            var innerIndex = inner.GroupBy(innerKeySelector, i => i).ToDictionary(g => g.Key);

            foreach (var outerPair in outerIndex)
            {
                var outerKey = outerPair.Key;
                var outerGrouping = outerPair.Value;
                if (!innerIndex.ContainsKey(outerKey))
                {
                    foreach (var outerValue in outerGrouping)
                    {
                        yield return resultSelector(outerValue, default(TInner));
                    }
                }
                else
                {
                    var innerGrouping = innerIndex[outerKey];

                    foreach (var outerValue in outerGrouping)
                    {
                        foreach (var innerValue in innerGrouping)
                        {
                            yield return resultSelector(outerValue, innerValue);
                        }
                    }
                }
            }

            var innerExceptKeys = innerIndex.Keys.Except(outerIndex.Keys);

            foreach (var innerExceptKey in innerExceptKeys)
            {
                var innerGrouping = innerIndex[innerExceptKey];
                foreach (var innerValue in innerGrouping)
                {
                    yield return resultSelector(default(TOuter), innerValue);
                }
            }
        }
    }
}
