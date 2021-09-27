#if NETCOREAPP3_1_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

using System;
using System.Globalization;

namespace RG.Ninja {
	public static class StringExtensions {
		public static bool StartsWith(this string s, string value,
#if NETCOREAPP3_1_OR_GREATER
			[NotNullWhen(true)]
#endif
			out string? remaining
		) {
			if (s.StartsWith(value)) {
				remaining = s.Substring(value.Length);
				return true;
			} else {
				remaining = null;
				return false;
			}
		}

		public static bool StartsWith(this string s, string value, StringComparison comparisonType,
#if NETCOREAPP3_1_OR_GREATER
			[NotNullWhen(true)]
#endif
			out string? remaining
		) {
			if (s.StartsWith(value, comparisonType)) {
				remaining = s.Substring(value.Length);
				return true;
			} else {
				remaining = null;
				return false;
			}
		}

		public static bool StartsWith(this string s, string value, bool ignoreCase, CultureInfo? culture,
#if NETCOREAPP3_1_OR_GREATER
			[NotNullWhen(true)]
#endif
			out string? remaining
		) {
			if (s.StartsWith(value, ignoreCase, culture)) {
				remaining = s.Substring(value.Length);
				return true;
			} else {
				remaining = null;
				return false;
			}
		}

		public static bool EndsWith(this string s, string value,
#if NETCOREAPP3_1_OR_GREATER
			[NotNullWhen(true)]
#endif
			out string? remaining
		) {
			if (s.EndsWith(value)) {
				remaining = s.Substring(0, s.Length - value.Length);
				return true;
			} else {
				remaining = null;
				return false;
			}
		}

		public static bool EndsWith(this string s, string value, StringComparison comparisonType,
#if NETCOREAPP3_1_OR_GREATER
			[NotNullWhen(true)]
#endif
			out string? remaining
		) {
			if (s.EndsWith(value, comparisonType)) {
				remaining = s.Substring(0, s.Length - value.Length);
				return true;
			} else {
				remaining = null;
				return false;
			}
		}

		public static bool EndsWith(this string s, string value, bool ignoreCase, CultureInfo? culture,
#if NETCOREAPP3_1_OR_GREATER
			[NotNullWhen(true)]
#endif
			out string? remaining
		) {
			if (s.EndsWith(value, ignoreCase, culture)) {
				remaining = s.Substring(0, s.Length - value.Length);
				return true;
			} else {
				remaining = null;
				return false;
			}
		}
	}
}
