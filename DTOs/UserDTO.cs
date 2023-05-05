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
        public String? UserName { get; set; }

        [Required(ErrorMessage = "Preencha o campo email")]
        [EmailAddress]
        public String? Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo senha")]
        public String? Password { get; set; }
    }
}