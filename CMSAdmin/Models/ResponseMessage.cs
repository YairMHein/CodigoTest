using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSAdmin.Models
{

    public class ResponseMessage
    {
        public string Message { get; set; }
        public int MessageType { get; set; }
        public string Promocode { get; set; }
        public string Token { get; set; }
    }

    public class UserCred
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
