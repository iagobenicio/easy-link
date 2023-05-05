using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace easy_link.Failures
{
    public class UserFailure : Exception
    {   
        public String? mensage { get; set; }
        public IEnumerable<IdentityError>? errors {get; set;}

        public UserFailure(IEnumerable<IdentityError>? errorsArgument, String? mensageArgument)
        {
            errors = errorsArgument;
            mensage = mensageArgument;
        }

    }
}