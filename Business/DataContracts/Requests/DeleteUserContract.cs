using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Requests
{
    [DataContract]
    public class DeleteUserContract
    {
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }
    }
}
