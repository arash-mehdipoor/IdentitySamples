using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySamples.Models.AAA.Data
{
    public class CreateUserModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CodeMeli { get; set; }
    }
}
