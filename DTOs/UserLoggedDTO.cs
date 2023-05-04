using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.DTOs
{
    public class UserLoggedDTO
    {   
        public String? Token {get;set;}
        public DateTime? Expires {get;set;}
    }
}