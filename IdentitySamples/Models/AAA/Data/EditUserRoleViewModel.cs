using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySamples.Models.AAA.Data
{
    public class EditUserRoleViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<string> UserRoles { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
