using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core
{
    public interface ISourceReader
    {
        Task<IEnumerable<House>> GetGardenHouses(CancellationToken cancellationToken);
        Task<IEnumerable<House>> GetHouses(CancellationToken cancellationToken);
    }
}