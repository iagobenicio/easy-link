using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.Failures
{
    public class RegisterPageException : Exception
    {
        public String? Menssage {get;set;}

        public RegisterPageException(string? message)
        {
            Menssage = message;
        }
    }
}