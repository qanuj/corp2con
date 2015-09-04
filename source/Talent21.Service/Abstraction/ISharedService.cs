using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISharedService : ISecuredService
    {
        IQueryable<Transaction> Transactions();
        string AddCredits(int num, string userId);
        int GetBalance(string userId);
        InvoiceViewModel TransactionById(int id);
    }

    public interface IViewService
    {
        void AddView(int id, string userAgent, string ipAddress);
    }
}