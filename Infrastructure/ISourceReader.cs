using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ISourceReader
    {
        Task<IEnumerable<House>> GetGardenHouses(CancellationToken cancellationToken);
        Task<IEnumerable<House>> GetHouses(CancellationToken cancellationToken);
    }
}