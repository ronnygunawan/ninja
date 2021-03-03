using System.Collections.Generic;

namespace RG.Ninja {
	public static class CollectionExtensions {
		public static void Add<T>(this ICollection<T> collection, IEnumerable<T> items) {
			foreach (T item in items) {
				collection.Add(item);
			}
		}
	}
}
