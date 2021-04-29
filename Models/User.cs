using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
    public class User
    {
 
        public string Password { get; set; }
        public string UserType { get; set; }
       public int Id { get; set; }
        public string Username { get; set; }

    }
}
