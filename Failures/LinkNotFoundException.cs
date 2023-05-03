using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.Failures
{
    public class LinkNotFoundException : Exception
    {
        public String? Menssage {get;set;}

        public LinkNotFoundException(string? message)
        {
            Menssage = message;
        }
    }
}