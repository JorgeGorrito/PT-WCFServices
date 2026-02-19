using System;
using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Requests
{
    [DataContract]
    public class UpdateUserContract
    {
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "birth_date")]
        public DateTime BirthDate { get; set; }

        [DataMember(Name = "gender")]
        public char Gender { get; set; }
    }
}
