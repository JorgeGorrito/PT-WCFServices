using System.Collections.Generic;

namespace PruebaTecnica.BDO.Entities
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
