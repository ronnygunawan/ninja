#if NETCOREAPP3_1_OR_GREATER
using System.Linq;
#endif

namespace System.Collections.Generic {
	public static class CollectionExtensions {
		public static void Add<T>(this ICollection<T> collection, IEnumerable<T> items) {
			foreach (T item in items) {
				collection.Add(item);
			}
		}

#if NETCOREAPP3_1_OR_GREATER
		public static void Add(this ICollection<int> collection, Range range) {
			collection.Add(range.AsEnumerable());
		}
#endif
	}
}
