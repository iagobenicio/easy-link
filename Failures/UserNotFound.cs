using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.Failures
{
    public class UserNotFound : Exception
    {
        public String? Menssage {get;set;}

        public UserNotFound(string? message)
        {
            Menssage = message;
        }
    }
}