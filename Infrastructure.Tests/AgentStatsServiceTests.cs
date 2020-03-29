using System.Linq;
using Core;
using Core.Domain;
using FluentAssertions;
using Moq;
using Xunit;

namespace Infrastructure.Tests
{
    public class AgentStatsServiceTests
    {
        private readonly House _houseWithGarden = House.CreateWithGarden("a", 1, "a");
        private readonly House _houseNoGarden1 = House.CreateWithNoGarden("b", 2, "b");
        private readonly House _houseNoGarden2 = House.CreateWithNoGarden("c", 2, "b");
        private readonly Mock<IHouseRepository> _mockRepo;
        private readonly AgentStatsService _sut;

        public AgentStatsServiceTests()
        {
            _mockRepo = new Mock<IHouseRepository>();
            _sut = new AgentStatsService(_mockRepo.Object);
        }
        
        [Fact]
        public void ShouldReturnEmptyListIfNoGardenAvailable()
        {
            _mockRepo.Setup(r => r.GetAll())
                .Returns(new[] {_houseNoGarden1});
            _sut
                .GetTopTenWithGarden()
                .Should()
                .BeEmpty();
        }
        
        [Fact]
        public void ShouldReturnAgentTopGardenAvailable()
        {
            _mockRepo.Setup(r => r.GetAll())
                .Returns(new[] {_houseNoGarden1, _houseWithGarden});
            _sut
                .GetTopTenWithGarden()
                .First()
                .Name
                .Should()
                .Be("a");
        }
        
        [Fact]
        public void ShouldReturnEmptyListIfNoneAvailable()
        {
            _mockRepo.Setup(r => r.GetAll())
                .Returns(Enumerable.Empty<House>());
            _sut
                .GetTopTenOverall()
                .Should()
                .BeEmpty();
        }
        
        [Fact]
        public void ShouldReturnAgentTopOverall()
        {
            _mockRepo.Setup(r => r.GetAll())
                .Returns(new[] {_houseNoGarden1, _houseNoGarden2, _houseWithGarden});
            
            _sut.GetTopTenOverall()
                .First()
                .Name
                .Should()
                .Be("b");
        }
    }
}