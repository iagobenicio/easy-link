using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace easy_link.Entities
{
    public class Page
    {   
        [ForeignKey("User")]
        public int Id { get; set; }
        [Required]
        public string? PageName { get; set; }
        [Required]
        public string? PageDescription { get; set; }
        public User? User { get; set; }
        public ICollection<Link>? Links { get; set; }
    }
}