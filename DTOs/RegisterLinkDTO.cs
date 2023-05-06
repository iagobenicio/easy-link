using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace easy_link.DTOs
{
    public class RegisterLinkDTO
    {   
        [Required(ErrorMessage = "Preencha o campo linkName")]
        public string? LinkName { get; set; }
        [Required(ErrorMessage = "Preencha o campo linkName")]
        public string? UrlDirection { get; set; }
    }
}