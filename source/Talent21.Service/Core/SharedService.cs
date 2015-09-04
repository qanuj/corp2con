using System.Collections.Generic;
using System.Linq;
using e10.Shared.Providers;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public abstract class SharedService : SecuredService, ISharedService
    {
        protected readonly ILocationRepository _locationRepository;
        protected readonly ITransactionRepository _transactionRepository;
        protected readonly IMemberRepository _memberRepository;
        protected readonly SellingOptions _sellingOptions;


        protected SharedService(ILocationRepository locationRepository, ITransactionRepository transactionRepository, SellingOptions sellingOptions, IUserProvider userProvider, IMemberRepository memberRepository) : base(userProvider)
        {
            _locationRepository = locationRepository;
            _transactionRepository = transactionRepository;
            _sellingOptions = sellingOptions;
            _memberRepository = memberRepository;
        }


        public int GetBalance(string userId)
        {
            return _transactionRepository.Balance(userId);
        }

        public abstract void AddView(int id, string userAgent, string ipAddress);

        public InvoiceViewModel TransactionById(int id)
        {
            var transaction=Transactions().FirstOrDefault(x => x.Id == id);
            if (transaction == null) return null;
            var member = _memberRepository.ByUserId(transaction.UserId);
            return new InvoiceViewModel()
            {
                TaxAmount = transaction.Amount*_sellingOptions.TaxRate/100,
                Tax = _sellingOptions.TaxRate,
                TaxName =_sellingOptions.TaxName,
                Id = transaction.Id,
                Created=transaction.Created,
                Total=transaction.Amount,
                UnitPrice= _sellingOptions.CreditPrice,
                Member = new MemberViewModel
                {
                    FirstName=member.FirstName,
                    LastName=member.LastName,
                    Location = member.Location!=null ? member.Location.Title : string.Empty,
                    AlternateNumber = member.AlternateNumber,
                    Mobile =member.Mobile,
                    Address=member.Address,
                    PinCode = member.PinCode,
                    Email = member.Email
                },
                Transactions = new List<Transaction> {
                    transaction
                }
            };
        }

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