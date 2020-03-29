using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SourceReaderService : GenericBackgroundService
    {
        private readonly ISourceReader _sourceReader;
        private readonly IHouseRepository _houseRepository;

        public SourceReaderService(ISourceReader sourceReader,
            IHouseRepository houseRepository
            )
        {
            _sourceReader = sourceReader;
            _houseRepository = houseRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var houses = 
                (await _sourceReader.GetGardenHouses(cancellationToken))
                .Concat(await _sourceReader.GetHouses(cancellationToken));

            _houseRepository.AddHouses(houses);
        }
    }
}