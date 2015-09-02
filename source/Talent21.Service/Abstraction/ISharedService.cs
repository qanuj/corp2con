using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISharedService
    {
        IQueryable<Transaction> Transactions();
        int GetBalance(string userId);
        void AddView(int id, string userAgent, string ipAddress);
    }
}