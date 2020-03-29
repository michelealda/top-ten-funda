using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Domain;

namespace Infrastructure
{
    public class AgentStatsService : IAgentStatsService
    {
        private readonly IHouseRepository _repository;

        public AgentStatsService(IHouseRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Stat> GetTopTenWithGarden()
            => GetTopTen(_repository
                .GetAll()
                .Where(h => h.HadGarden));

        public IEnumerable<Stat> GetTopTenOverall()
            => GetTopTen(_repository
                .GetAll());

        private static IEnumerable<Stat> GetTopTen(IEnumerable<House> houses)
            => houses.GroupBy(h => h.AgentId)
                .Select(g => new Stat(g.First().AgentName, g.Count()))
                .OrderByDescending(s => s.Value)
                .Take(10);
    }
}