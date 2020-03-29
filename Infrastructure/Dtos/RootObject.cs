using System;
using System.Collections.Generic;

namespace Infrastructure.Dtos
{
    public class Object
    {
        public string Id { get; set; }
        public int MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
    }

    public class Paging
    {
        public int AantalPaginas { get; set; }
        public int HuidigePagina { get; set; }
    }

    public class RootObject
    {
        public int Website { get; set; }
        public IEnumerable<Object> Objects { get; set; }
        public Paging Paging { get; set; }
        public int TotaalAantalObjecten { get; set; }
    }
}