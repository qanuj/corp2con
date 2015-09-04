using System.Linq;
using e10.Shared.Providers;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public abstract class SharedService : ISharedService
    {
        public string CurrentUserId
        {
            get
            {
                return _userProvider.UserName;
            }
        }

        protected readonly ILocationRepository _locationRepository;
        protected readonly ITransactionRepository _transactionRepository;
        protected readonly SellingOptions _sellingOptions;
        protected readonly IUserProvider _userProvider;


        protected SharedService(ILocationRepository locationRepository, ITransactionRepository transactionRepository, SellingOptions sellingOptions, IUserProvider userProvider)
        {
            _locationRepository = locationRepository;
            _transactionRepository = transactionRepository;
            _sellingOptions = sellingOptions;
            _userProvider = userProvider;
        }


        public int GetBalance(string userId)
        {
            return _transactionRepository.Balance(userId);
        }

        public abstract void AddView(int id, string userAgent, string ipAddress);

        public IQueryable<Transaction> Transactions()
        {
            return _transactionRepository.All.Where(x => x.UserId == CurrentUserId && x.IsSuccess);
        }

        public string AddCredits(int num, string userId)
        {
            var transction = new Payment
            {
                Code = Transaction.GenerateTransactionId(),
                UserId = userId,
                Name = string.Format("{0} Credits Purchased", num),
                Credit = num,
                Amount = (num * _sellingOptions.CreditPrice)
            };
            _transactionRepository.Create(transction);
            _transactionRepository.SaveChanges();

            return transction.Code;
        }

        protected Location FindLocation(string address,int locationId)
        {
            var location = string.IsNullOrWhiteSpace(address) ? _locationRepository.ById(locationId) : _locationRepository.ByTitle(address);
            if (location != null) return location;

            return new Location
            {
                Title = address
            };
        }
    }
}