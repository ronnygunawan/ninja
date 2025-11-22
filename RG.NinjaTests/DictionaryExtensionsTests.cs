using System.Collections.Generic;
using Xunit;
using Shouldly;

namespace RG.NinjaTests {
	public class DictionaryExtensionsTests {
		[Fact]
		public void CanUseDictionaryInDictionaryInitializer() {
			Dictionary<int, string> dictionary = new() {
				{ 1, "Satu" },
				{ 2, "Dua" }
			};
			Dummy dummy = new() {
				Items = { dictionary }
			};
			dummy.Items.ShouldContainKeyAndValue(1, "Satu");
			dummy.Items.ShouldContainKeyAndValue(2, "Dua");
		}

		[Fact]
		public void CanUseSelectorInDictionaryInitializer() {
			string[] values = new[] { "Satu", "Dua" };
			Dummy dummy = new() {
				Items = {
					{ values, v => v.Length },
					{ values, v => v.Length - 2, v => v + v }
				}
			};
			dummy.Items.ShouldContainKeyAndValue(4, "Satu");
			dummy.Items.ShouldContainKeyAndValue(3, "Dua");
			dummy.Items.ShouldContainKeyAndValue(2, "SatuSatu");
			dummy.Items.ShouldContainKeyAndValue(1, "DuaDua");
		}

		[Fact]
		public void CanMixDictionaryAndValuesInDictionaryInitializer() {
			Dictionary<int, string> dictionary = new() {
				{ 1, "Satu" },
				{ 2, "Dua" }
			};
			Dummy dummy = new() {
				Items = {
					{ dictionary },
					{ 3, "Tiga" }
				}
			};
			dummy.Items.ShouldContainKeyAndValue(1, "Satu");
			dummy.Items.ShouldContainKeyAndValue(2, "Dua");
			dummy.Items.ShouldContainKeyAndValue(3, "Tiga");
		}

		private class Dummy {
			public Dictionary<int, string> Items { get; } = new();
		}
	}
}
