using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Responses
{
    [DataContract]
    public class UpdateUserResponse
    {
        [DataMember(Name = "user_updated_id", Order = 0)]
        public int UserUpdatedId { get; set; }
    }
}
