using Microsoft.AspNet.Identity;

namespace e10.Shared.Providers
{
    public class CurrentUserProvider : IUserProvider
    {
        public string UserName
        {
            get
            {
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (identity != null) return identity.GetUserId();
                return string.Empty;
            }
        }
    }
}