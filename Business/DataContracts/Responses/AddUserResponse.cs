using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Responses
{
    [DataContract]
    public class AddUserResponse
    {
        [DataMember(Name = "user_added_id", Order = 0)]
        public int UserAddedID { get; set; }
    }
}