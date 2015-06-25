using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e10.Shared.Data
{
    public abstract class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserLogin, UserRole, IdentityUserClaim>
    {
        protected ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
