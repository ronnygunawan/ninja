#if NETCOREAPP3_1_OR_GREATER
using System.Collections.Generic;

namespace System.Linq {
	public static class RangeEnumerableExtensions {
		public static IEnumerable<int> AsEnumerable(this Range range) {
			if (range.Start.IsFromEnd) throw new InvalidOperationException("IsFromEnd is not allowed.");
			if (range.End.IsFromEnd) throw new InvalidOperationException("IsFromEnd is not allowed.");
			for (int i = range.Start.Value; i < range.End.Value; i++) {
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
