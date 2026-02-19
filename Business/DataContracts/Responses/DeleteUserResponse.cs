using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Responses
{
    [DataContract]
    public class DeleteUserResponse
    {
        [DataMember(Name = "user_deleted_id", Order = 0)]
        public int UserDeletedId { get; set; }
    }
}
