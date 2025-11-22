using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace RG.NinjaTests {
	public class EnumerableExtensionsTests {
		[Fact]
		public void CanZipIndexUsingWithIndexMethod() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<(string Item, int Index)> result = items.WithIndex().ToList();
			result.ShouldBe(new[] { ("alpha", 0), ("beta", 1), ("gamma", 2) }, ignoreOrder: false);
		}

		[Fact]
		public void CanZipIndexUsingRange() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<(string Item, int Index)> result = items.Zip(..).ToList();
			result.ShouldBe(new[] { ("alpha", 0), ("beta", 1), ("gamma", 2) }, ignoreOrder: false);

			result = items.Zip(..^1).ToList();
			result.ShouldBe(new[] { ("alpha", 0), ("beta", 1) }, ignoreOrder: false);

			result = items.Zip(^1..).ToList();
			result.ShouldBe(new[] { ("alpha", 2) }, ignoreOrder: false);
		}

		[Fact]
		public void CanTakeUsingRange() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<string> result = items.Take(..).ToList();
			result.ShouldBe(new[] { "alpha", "beta", "gamma" }, ignoreOrder: false);
			result = items.Take(1..).ToList();
			result.ShouldBe(new[] { "beta", "gamma" }, ignoreOrder: false);
			result = items.ToArray()[1..].ToList();
			result.ShouldBe(new[] { "beta", "gamma" }, ignoreOrder: false);
			result = items.Take(^1..).ToList();
			result.ShouldBe(new[] { "gamma" }, ignoreOrder: false);
			result = items.ToArray()[^1..].ToList();
			result.ShouldBe(new[] { "gamma" }, ignoreOrder: false);
			result = items.Take(..1).ToList();
			result.ShouldBe(new[] { "alpha" }, ignoreOrder: false);
			result = items.ToArray()[..1].ToList();
			result.ShouldBe(new[] { "alpha" }, ignoreOrder: false);
			result = items.Take(..^2).ToList();
			result.ShouldBe(new[] { "alpha" }, ignoreOrder: false);
			result = items.ToArray()[..^2].ToList();
			result.ShouldBe(new[] { "alpha" }, ignoreOrder: false);
		}

		[Fact]
		public void CanSkipUsingRange() {
			List<string> items = new() { "alpha", "beta", "gamma" };
			List<string> result = items.Skip(..).ToList();
			result.ShouldBeEmpty();
			result = items.Skip(1..).ToList();
			result.ShouldBe(new[] { "alpha" }, ignoreOrder: false);
			result = items.Skip(^1..).ToList();
			result.ShouldBe(new[] { "alpha", "beta" }, ignoreOrder: false);
			result = items.Skip(..1).ToList();
			result.ShouldBe(new[] { "beta", "gamma" }, ignoreOrder: false);
			result = items.Skip(..^2).ToList();
			result.ShouldBe(new[] { "beta", "gamma" }, ignoreOrder: false);
			result = items.Skip(1..2).ToList();
			result.ShouldBe(new[] { "alpha", "gamma" }, ignoreOrder: false);
		}
	}
}
