using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace e10.Shared.Data
{
    public class BasicConfiguration : DbConfiguration
    {
        public BasicConfiguration()
        {
            AddInterceptor(new BasicCommandInterceptor());
        }
    }
}
