using System.Collections.Generic;

namespace System.Linq {
	public static class EnumerableExtensions {
		public static IEnumerable<(TSource Item, int Index)> WithIndex<TSource>(this IEnumerable<TSource> source) {
			int i = 0;
			foreach(TSource item in source) {
				yield return (item, i++);
			}
		}

#if !NETSTANDARD2_0
		public static IEnumerable<(TSource Item, int Index)> Zip<TSource>(this IEnumerable<TSource> source, Range range) {
			int count = source is IReadOnlyCollection<TSource> readonlyCollection
				? readonlyCollection.Count
				: source is ICollection<TSource> collection
					? collection.Count
					: source.Count();
			(int offset, int length) = range.GetOffsetAndLength(count);
			return source.Zip(Enumerable.Range(
				start: offset,
				count: length
			));
		}
#endif

#if !NETSTANDARD2_0
		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, Range range) {
			int count = source is IReadOnlyCollection<TSource> readonlyCollection
				? readonlyCollection.Count
				: source is ICollection<TSource> collection
					? collection.Count
					: source.Count();
			int start = range.Start.GetOffset(count);
			int end = range.End.GetOffset(count);
			return source.Skip(start).SkipLast(count - end);
		}
#endif

#if !NETSTANDARD2_0
		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, Range range) {
			int count = source is IReadOnlyCollection<TSource> readonlyCollection
				? readonlyCollection.Count
				: source is ICollection<TSource> collection
					? collection.Count
					: source.Count();
			int start = range.Start.GetOffset(count);
			int end = range.End.GetOffset(count);
			return source.Take(start).Concat(source.TakeLast(count - end));
		}
#endif
	}
}
