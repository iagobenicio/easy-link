using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace easy_link.Entities
{
    public class Link
    {
        public int Id { get; set; }
        [Required]
        public string? LinkName { get; set; }
        [Required]
        public string? UrlDirection { get; set; }
        public int ClicksCount { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}