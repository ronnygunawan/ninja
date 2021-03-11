#if !NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Linq;

namespace RG.Ninja {
	public static class RangeEnumerableExtensions {
		public static IEnumerable<int> AsEnumerable(this Range range) {
			int start = range.Start.IsFromEnd
				? range.Start.Value + 1
				: range.Start.Value;
			int end = range.End.IsFromEnd
				? range.End.Value - 1
				: range.End.Value;
			for (int i = start; i <= end; i++) {
				yield return i;
			}
		}

		public static IEnumerator<int> GetEnumerator(this Range range) {
			return range.AsEnumerable().GetEnumerator();
		}

		public static IEnumerable<TResult> Cast<TResult>(this Range range) {
			return range.AsEnumerable().Cast<TResult>();
		}

		public static IEnumerable<TResult> Select<TResult>(this Range range, Func<int, TResult> selector) {
			return range.AsEnumerable().Select(selector);
		}

		public static IEnumerable<TResult> SelectMany<TResult>(this Range range, Func<int, Range> otherRangeSelector, Func<int, int, TResult> resultSelector) {
			foreach (int i in range) {
				foreach (int j in otherRangeSelector.Invoke(i)) {
					yield return resultSelector.Invoke(i, j);
				}
			}
		}

		public static IEnumerable<TResult> SelectMany<TCollection, TResult>(this Range range, Func<int, IEnumerable<TCollection>> collectionSelector, Func<int, TCollection, TResult> resultSelector) {
			foreach (int i in range) {
				foreach (TCollection item in collectionSelector.Invoke(i)) {
					yield return resultSelector.Invoke(i, item);
				}
			}
		}

		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Range> rangeSelector, Func<TSource, int, TResult> resultSelector) {
			foreach (TSource item in source) {
				foreach(int i in rangeSelector.Invoke(item)) {
					yield return resultSelector.Invoke(item, i);
				}
			}
		}
	}
}
#endif
