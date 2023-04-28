using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace easy_link.Entities
{
    public class User : IdentityUser<int>
    {   
        [Required]
        public string? Description { get; set; }
        public byte[]? ImageProfile { get; set; } 
        public ICollection<Link>? Links { get; set; }
    }
}