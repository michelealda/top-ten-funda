using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class InMemoryHouseRepository : IHouseRepository
    {
        private readonly ILogger<InMemoryHouseRepository> _logger;
        private readonly Dictionary<string, House> _dict;
        public InMemoryHouseRepository(ILogger<InMemoryHouseRepository> logger)
        {
            _logger = logger;
            _dict = new Dictionary<string, House>();
        }
        
        public void AddHouses(IEnumerable<House> houses)
        {
            houses
                .ToList()
                .ForEach(h => _dict.TryAdd(h.Id, h));
            
            _logger.LogInformation($"Total is now {_dict.Count}");
        }

        public IEnumerable<House> GetAll()
            => _dict.Values;
    }
}