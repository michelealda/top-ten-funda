using System.Collections.Generic;
using Core.Domain;

namespace Core
{
    public interface IHouseRepository
    {
        void AddHouses(IEnumerable<House> houses);

        IEnumerable<House> GetAll();
    }
}