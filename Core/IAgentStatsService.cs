using System.Collections.Generic;
using Core.Domain;

namespace Core
{
    public interface IAgentStatsService
    {
        IEnumerable<Stat> GetTopTenWithGarden();
        IEnumerable<Stat> GetTopTenOverall();

    }
}