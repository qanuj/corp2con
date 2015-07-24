using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISharedService
    {
        void AddView(int id, string userAgent, string ipAddress);
    }
}