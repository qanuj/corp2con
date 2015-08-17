using System;
using System.Linq;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public abstract class SharedService : ISecuredService
    {
        public string CurrentUserId { set; protected get; }

        protected readonly ILocationRepository _locationRepository;
        protected readonly ITransactionRepository _transactionRepository;

        protected SharedService(ILocationRepository locationRepository, ITransactionRepository transactionRepository)
        {
            _locationRepository = locationRepository;
            _transactionRepository = transactionRepository;
        }

        public IQueryable<TransactionViewModel> Transactions()
        {
            return _transactionRepository.All.Where(x=>x.UserId==CurrentUserId).Select(x=> new TransactionViewModel
            {
                Id=x.Id,
                Amount = x.Amount,
                Credit = x.Credit,
                Code = x.Code,
                IsSuccess = x.IsSuccess,
                PaymentCapture = x.PaymentCapture,
                Reason = x.Reason,
                Created = x.Created
            });
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