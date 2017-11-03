using System;
using SIG.Data.Enums;

namespace SIG.Data.Entity.Identity
{
    public class UserSafety
    {
        public int Id { get; set; }       
        public string Username { get; set; }
        public Guid Code { get; set; }
        public int Timeout { get; set; }
        public EmailType EmailType { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
