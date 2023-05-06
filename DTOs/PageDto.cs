using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Entities;

namespace easy_link.DTOs
{
    public class PageDto
    {   
        public byte[]? ImageProfile {get;set;}
        public String? PageName {get;set;}
        public String? PageDescription {get;set;}
        public ICollection<LinkDTO>? Links {get;set;}

    }
}