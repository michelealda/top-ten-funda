using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class InMemoryHouseRepository : IHouseRepository
    {
        private readonly Dictionary<string, House> _dict;
        public InMemoryHouseRepository()
        {
            _dict = new Dictionary<string, House>();
            
        }
        
        public void AddHouses(IEnumerable<House> houses)
        {
            houses
                .ToList()
                .ForEach(h => _dict.TryAdd(h.Id, h));
        }

        public IEnumerable<House> GetAll()
            => _dict.Values;
    }
}