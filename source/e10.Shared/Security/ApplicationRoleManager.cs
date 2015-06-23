using System.Collections.Generic;
using System.Linq;
using System.Text;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace e10.Shared.Security
{
    public class ApplicationRoleManager : RoleManager<Role>
    {
        public ApplicationRoleManager(ApplicationRoleStore store)
            : base(store)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, ApplicationRoleStore store)
        {
            return new ApplicationRoleManager(store);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    // Configure the application sign-in manager which is used in this application.
}
