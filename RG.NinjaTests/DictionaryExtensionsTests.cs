using System.Collections.Generic;
using Xunit;
using FluentAssertions;

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
			dummy.Items.Should().Contain(new Dictionary<int, string> {
				{ 1, "Satu" }, { 2, "Dua" }
			});
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
			dummy.Items.Should().Contain(new Dictionary<int, string> {
				{ 4, "Satu" }, { 3, "Dua" },
				{ 2, "SatuSatu" }, { 1, "DuaDua" }
			});
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
			dummy.Items.Should().Contain(new Dictionary<int, string> {
				{ 1, "Satu" }, { 2, "Dua" },
				{ 3, "Tiga" }
			});
		}

		private class Dummy {
			public Dictionary<int, string> Items { get; } = new();
		}
	}
}
