using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.Failures
{
    public class UpdatePageException : Exception
    {
        public String? Menssage {get;set;}

        public UpdatePageException(string? message)
        {
            Menssage = message;
        }
    }
}