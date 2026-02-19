using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Responses
{
    [DataContract]
    public class GetUsersPaginatedResponse
    {
        [DataMember(Name = "users", Order = 0)]
        public List<UserDto> Users { get; set; } = new List<UserDto>();

        [DataMember(Name = "total_users", Order = 1)]
        public int TotalUsers { get; set; }

        [DataMember(Name = "current_page", Order = 2)]
        public int CurrentPage { get; set; }

        [DataMember(Name = "page_size", Order = 3)]
        public int PageSize { get; set; }
    }
}
