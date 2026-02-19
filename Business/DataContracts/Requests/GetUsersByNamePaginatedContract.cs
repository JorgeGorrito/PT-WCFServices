using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Requests
{
    [DataContract]
    public class GetUsersByNamePaginatedContract
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "order_desc")]
        public bool OrderIdDesc { get; set; } = true;

        [DataMember(Name = "page_size")]
        public int PageSize { get; set; } = 10;

        [DataMember(Name = "current_page")]
        public int CurrentPage { get; set; } = 1;
    }
}
