using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Entities;

namespace easy_link.DTOs
{
    public class DashboardDTO
    {
        public int Id { get; set; }
        public string? PageName { get; set; }
        public string? PageDescription { get; set; }
        public byte[]? ImageProfile { get; set; }
        public ICollection<Link>? Links { get; set; }
    }
}