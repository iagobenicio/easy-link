using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.DTOs
{
    public class DashboardLinkDTO : LinkDTO
    {
        public int Id {get;set;}
        public int ClicksCount { get; set; }
        public int PageId { get; set; } 
    }
}