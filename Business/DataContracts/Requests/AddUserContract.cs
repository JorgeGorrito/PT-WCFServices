using System;
using System.Runtime.Serialization;

namespace PruebaTecnica.Business.DataContracts.Requests
{
    [DataContract]
    public class AddUserContract
    {
        [DataMember(Name="name")]
        public string Name {  set; get; } = String.Empty;

        [DataMember(Name = "birth_date")]
        public DateTime BirthDate { set; get; } = DateTime.MinValue;

        [DataMember(Name = "gender")]
        public char Gender { get; set; } = 'F';
    }
}