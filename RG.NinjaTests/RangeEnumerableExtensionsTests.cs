using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace RG.NinjaTests {
	public class RangeEnumerableExtensionsTests {
		[Fact]
		public void CanUseRangeInForeach() {
			List<int> result = new();
			foreach(int i in ..10) {
				result.Add(i);
			}
			result.Should().ContainInOrder(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
		}

		[Fact]
		public void CanUseRangeInLinq() {
			List<int> result = new();
			foreach (int i in from int i in 0..6
							  where i % 2 == 0
							  select i) {
				result.Add(i);
			}
			result.Should().ContainInOrder(0, 2, 4);
		}
	}
}
