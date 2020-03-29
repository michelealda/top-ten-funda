using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Core;
[assembly: InternalsVisibleTo("Infrastructure.Tests")]
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
            var withGarden = (await _sourceReader.GetGardenHouses(cancellationToken)).ToList();
            var allHouses = (await _sourceReader.GetHouses(cancellationToken)).ToList();

            _houseRepository.AddHouses(
                withGarden.Concat(allHouses)
            );
        }
    }
}