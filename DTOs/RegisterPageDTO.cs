using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace easy_link.DTOs
{
    public class RegisterPageDTO
    {   
        [Required(ErrorMessage = "Preencha o campo pagename")]
        public string? PageName { get; set; }
        
        [Required(ErrorMessage = "Preencha o campo pagedescription")]
        public string? PageDescription { get; set; } 
    }
}