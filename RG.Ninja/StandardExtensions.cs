using System.Threading.Tasks;

namespace System {
	public static class StandardExtensions {
		public static T Setup<T>(this T it, Action<T> setupAction) {
			setupAction.Invoke(it);
			return it;
		}

		public static async Task<T> SetupAsync<T>(this T it, Func<T, Task> asyncSetupAction) {
			await asyncSetupAction.Invoke(it).ConfigureAwait(false);
			return it;
		}

		public static TResult Let<T, TResult>(this T it, Func<T, TResult> selector) {
			return selector.Invoke(it);
		}

		public static Task<TResult> LetAsync<T, TResult>(this T it, Func<T, Task<TResult>> asyncSelector) {
			return asyncSelector.Invoke(it);
		}
	}
}
