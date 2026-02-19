using System;
using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Responses
{
    [DataContract]
    public class UserDto
    {
        [DataMember(Name = "id", Order = 0)]
        public int ID { get; set; }

        [DataMember(Name = "name", Order = 1)]
        public string Name { get; set; }

        [DataMember(Name = "birth_date", Order = 2)]
        public DateTime BirthDate { get; set; }

        [DataMember(Name = "gender", Order = 3)]
        public char Gender { get; set; }
    }
}
