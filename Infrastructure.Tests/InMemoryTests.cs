using System.Linq;
using Core.Domain;
using FluentAssertions;
using Xunit;

namespace Infrastructure.Tests
{
    public class InMemoryTests
    {
        private readonly InMemoryHouseRepository _inMemoryHouseRepository;

        private readonly House _aWithGarden = House.CreateWithGarden("a", 1, "1");
        private readonly House _aNoGarden = House.CreateWithNoGarden("a", 1, "1");
        private readonly House _bNoGarden = House.CreateWithGarden("b", 1, "1");
        
        public InMemoryTests()
        {
            _inMemoryHouseRepository = new InMemoryHouseRepository();
        }

        [Fact]
        public void ShouldAddOnlyHousesWithGardenOnce()
        {
            _inMemoryHouseRepository.AddHouses(new[]
            {
                _aWithGarden,
                _aNoGarden,
                _bNoGarden
            });

            _inMemoryHouseRepository.GetAll()
                .Should()
                .BeEquivalentTo(_aWithGarden, _bNoGarden);
        }
        
        [Fact]
        public void ShouldKeepPrecedence()
        {
            _inMemoryHouseRepository.AddHouses(new[]
            {
                _aNoGarden,
                _aWithGarden
            });

            _inMemoryHouseRepository.GetAll()
                .Should()
                .HaveCount(1);

            _inMemoryHouseRepository.GetAll()
                .First()
                .HadGarden
                .Should()
                .BeFalse();
        }
    }
}