using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.Failures
{
    public class PageNotFoundException : Exception
    {   
        
        public String? Menssage {get;set;}

        public PageNotFoundException(string? message)
        {
            Menssage = message;
        }


    }
}