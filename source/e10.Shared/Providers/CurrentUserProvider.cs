using System.Web;
using e10.Shared.Data.Abstraction;
using e10.Shared.Security;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Providers
{
    public class CurrentUserProvider : IUserProvider
    {
        private readonly ApplicationUserManager _userManager;

        public CurrentUserProvider(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public string UserName
        {
            get
            {
                var identity = HttpContext.Current.User.Identity;
                if (identity != null) return identity.GetUserId();
                return string.Empty;
            }
        }

        public string UserIdByEmail(string email)
        {
             var user=_userManager.FindByEmail(email);
            if (user != null) return user.Id;
            return string.Empty;
        }

        public string UserEmailById(string id)
        {
            var user = _userManager.FindById(id);
            if (user != null) return user.Email;
            return string.Empty;
        }
    }
}