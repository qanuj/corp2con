using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data;

namespace Talent21.Data
{
    public class ApplicationDataContext : ApplicationDbContext
    {
       public ApplicationDataContext():base("DefaultConnection"){}
       public static ApplicationDataContext Create()
       {
           return new ApplicationDataContext();
       }
    }
}
