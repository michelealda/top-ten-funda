using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ISourceReader
    {
        Task PopulateFromSource();
    }
}