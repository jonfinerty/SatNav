using System;
using System.Collections.Generic;
using System.Data;

namespace SatNav
{
    internal static class PairedIEnumerableExtension
    {
        internal static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> enumerable)
        {
            var enumerator = enumerable.GetEnumerator();

            var notEmpty = enumerator.MoveNext();

            if (notEmpty == false)
            {
                yield break;
            }

            var nextItem = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var currentItem = nextItem;
                nextItem = enumerator.Current;

                yield return new Tuple<T, T>(currentItem, nextItem);
            }
        }

        internal static bool IsPositive(this int number)
        {
            return number > 0;
        }
    }
}