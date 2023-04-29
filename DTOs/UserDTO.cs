using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.DTOs
{
    public class UserDTO
    {   
        [Required(ErrorMessage = "Preencha o campo nome de usuário")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Preencha o campo email")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo senha")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Preencha o campo descrição")]        
        public string? Description { get; set; }
    }
}