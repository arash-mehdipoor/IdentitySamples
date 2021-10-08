using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySamples.Models.AAA.Data
{
    public class AAADbContext : IdentityDbContext<IdentityUser>
    {
        public AAADbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
