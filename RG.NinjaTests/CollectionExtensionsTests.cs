using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace RG.NinjaTests {
	public class CollectionExtensionsTests {
		[Fact]
		public void CanUseListInCollectionInitializer() {
			List<int> list = new() { 1, 2, 3 };
			Dummy dummy = new() {
				Items = { list }
			};
			dummy.Items.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: false);
		}

		[Fact]
		public void CanUseRangeInCollectionInitializer() {
			Dummy dummy = new() {
				Items = { 1..3 }
			};
			dummy.Items.ShouldBe(new[] { 1, 2 }, ignoreOrder: false);
		}

		[Fact]
		public void CanMixListAndValuesInCollectionInitializer() {
			List<int> list = new() { 1, 2, 3 };
			Dummy dummy = new() {
				Items = {
					list,
					4, 5, 6,
					from i in Enumerable.Range(0, 10)
					where i % 2 == 0
					select i + 7
				}
			};
			dummy.Items.ShouldBe(new[] {
				1, 2, 3,
				4, 5, 6,
				7, 9, 11, 13, 15
			}, ignoreOrder: false);
		}

		private class Dummy {
			public List<int> Items { get; } = new();
		}
	}
}
