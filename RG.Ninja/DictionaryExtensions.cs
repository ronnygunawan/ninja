namespace System.Collections.Generic {
	public static class DictionaryExtensions {
		public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value) {
			key = keyValuePair.Key;
			value = keyValuePair.Value;
		}

		public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items) {
			foreach ((TKey key, TValue value) in items) {
				dictionary.Add(key, value);
			}
		}

		public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TValue> values, Func<TValue, TKey> keySelector) {
			foreach (TValue value in values) {
				dictionary.Add(keySelector(value), value);
			}
		}

		public static void Add<TSource, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector) {
			foreach (TSource item in items) {
				dictionary.Add(keySelector(item), elementSelector(item));
			}
		}
	}
}
