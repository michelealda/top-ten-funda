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
        public IEnumerable<Stat> GetTop()
            => _agentStatsService.GetTopTenOverall();
        
        [HttpGet("garden")]
        public IEnumerable<Stat> GetTopWithGarden()
            => _agentStatsService.GetTopTenWithGarden();
    }
}