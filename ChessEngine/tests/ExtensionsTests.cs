using System.Collections.Generic;
using FluentAssertions;
using ChessEngine.Extensions;
using Xunit;
using System.Linq;

namespace ChessEngine.tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void AddMultiple_WhenGivenAnObjectTypeAndN_ReturnsListOfNObjects()
        {
            var list = new List<string>();
            list.AddMultiple(10, string.Empty);
            list.Count.Should().Be(10);
        }
        [Fact]
        public void AddMultiple_WhenGivenTwoObjectsAndCount2_ReturnsListOf2Objects()
        {
            var list = new List<string>();
            list.AddMultiple(2, "a", "b");
            list.Count.Should().Be(2);
            list.First().Should().Be("a");
            list.Last().Should().Be("b");
        }
    }
}