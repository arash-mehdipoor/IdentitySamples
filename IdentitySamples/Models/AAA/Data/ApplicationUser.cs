using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySamples.Models.AAA.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string CodeMeli { get; set; }
    }
}
