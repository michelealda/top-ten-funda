using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IAgentStatsService _agentStatsService;

        public StatsController(IAgentStatsService agentStatsService)
        {
            _agentStatsService = agentStatsService;
        }

        [HttpGet("overall")]
        public string GetTop()
            => ConvertToTabular(
                _agentStatsService
                    .GetTopTenOverall());


        [HttpGet("garden")]
        public string GetTopWithGarden()
            => ConvertToTabular(
                _agentStatsService
                    .GetTopTenWithGarden());
        
        private static string ConvertToTabular(IEnumerable<Stat> stats)
        => stats
            .Select((stat, i) => $"{i+1}\t{stat.Name}\t{stat.Value}{Environment.NewLine}" )
            .Aggregate("", (a, b)=> a+b);
    }
}