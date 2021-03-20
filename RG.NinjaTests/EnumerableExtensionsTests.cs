using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace RG.NinjaTests {
	public class EnumerableExtensionsTests {
		[Fact]
		public void CanZipIndexUsingWithIndexMethod() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<(string Item, int Index)> result = items.WithIndex().ToList();
			result.Should().ContainInOrder(("alpha", 0), ("beta", 1), ("gamma", 2));
		}

		[Fact]
		public void CanZipIndexUsingRange() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<(string Item, int Index)> result = items.Zip(..).ToList();
			result.Should().ContainInOrder(("alpha", 0), ("beta", 1), ("gamma", 2));

			result = items.Zip(..^1).ToList();
			result.Should().ContainInOrder(("alpha", 0), ("beta", 1));

			result = items.Zip(^1..).ToList();
			result.Should().ContainInOrder(("alpha", 2));
		}

		[Fact]
		public void CanTakeUsingRange() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<string> result = items.Take(..).ToList();
			result.Should().ContainInOrder("alpha", "beta", "gamma");
			result = items.Take(1..).ToList();
			result.Should().ContainInOrder("beta", "gamma");
			result = items.Take(^1..).ToList();
			result.Should().ContainInOrder("gamma");
			result = items.Take(..1).ToList();
			result.Should().ContainInOrder("alpha", "beta");
			result = items.Take(..^2).ToList();
			result.Should().ContainInOrder("alpha");
		}

		[Fact]
		public void CanSkipUsingRange() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<string> result = items.Skip(..).ToList();
			result.Should().ContainInOrder();
			result = items.Skip(1..).ToList();
			result.Should().ContainInOrder("alpha");
			result = items.Skip(^1..).ToList();
			result.Should().ContainInOrder("alpha", "beta");
			result = items.Skip(..1).ToList();
			result.Should().ContainInOrder("gamma");
			result = items.Skip(..^2).ToList();
			result.Should().ContainInOrder("beta", "gamma");
			result = items.Skip(1..1).ToList();
			result.Should().ContainInOrder("alpha", "gamma");
		}
	}
}
