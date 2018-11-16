using System.Runtime.Serialization;

namespace UserRegistration.DataModel
{
    [DataContract]
    public class User
    {
        [DataMember]
        public  int UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
