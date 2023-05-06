using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace easy_link.Entities
{
    public class User : IdentityUser<int>
    {   
        public Page? Page { get; set; } 
    }
}