using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class SourceReaderService : GenericBackgroundService
    {
        private readonly ISourceReader _sourceReader;
        private readonly IHouseRepository _houseRepository;
        private readonly ILogger<SourceReaderService> _logger;

        public SourceReaderService(ISourceReader sourceReader,
            IHouseRepository houseRepository,
            ILogger<SourceReaderService> logger
            )
        {
            _sourceReader = sourceReader;
            _houseRepository = houseRepository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var withGarden = (await _sourceReader.GetGardenHouses(cancellationToken)).ToList();
            
            _logger.LogInformation($"Retrieved {withGarden.Count()} houses with garden");
            
            var allHouses = (await _sourceReader.GetHouses(cancellationToken)).ToList();
            
            _logger.LogInformation($"Retrieved {allHouses.Count()} houses");
            _houseRepository.AddHouses(withGarden);
            _houseRepository.AddHouses(allHouses);
        }
    }
}