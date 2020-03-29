using System.Collections.Generic;

namespace Infrastructure
{
    public interface IHouseRepository
    {
        void AddHouses(IEnumerable<House> houses);

        IEnumerable<House> GetAll();
    }
}