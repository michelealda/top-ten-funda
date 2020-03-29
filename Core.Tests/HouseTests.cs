using System;
using Core.Domain;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class HouseTests
    {
        [Fact]
        public void ShouldCreateHouseWithGarden()
            => House.CreateWithGarden("a", 1, "aa")
                .HadGarden
                .Should()
                .BeTrue();
        
        [Fact]
        public void ShouldCreateHouseWithNoGarden()
            => House.CreateWithNoGarden("a", 1, "aa")
                .HadGarden
                .Should()
                .BeFalse();
    }
}