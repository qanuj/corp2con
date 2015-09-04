using System.Web;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Providers
{
    public class CurrentUserProvider : IUserProvider
    {
        public string UserName
        {
            get
            {
                var identity = HttpContext.Current.User.Identity;
                if (identity != null) return identity.GetUserId();
                return string.Empty;
            }
        }
    }
}