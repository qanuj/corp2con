using e10.Shared.Data.Abstraction;

namespace e10.Shared.Providers
{
    public interface IUserProvider
    {
        string UserName { get; }
        string UserIdByEmail(string email);
    }
}