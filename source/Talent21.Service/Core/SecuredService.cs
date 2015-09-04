using e10.Shared.Providers;
using Talent21.Service.Abstraction;

namespace Talent21.Service.Core
{
    public abstract class SecuredService : ISecuredService
    {
        protected readonly IUserProvider _userProvider;

        protected SecuredService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public string CurrentUserId
        {
            get
            {
                return _userProvider.UserName;
            }
        }
    }
}