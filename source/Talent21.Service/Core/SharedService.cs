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

        public IQueryable<Transaction> Transactions()
        {
            return _transactionRepository.All;
        } 

        protected Location FindLocation(string address)
        {
            var location = _locationRepository.ByTitle(address);
            if (location != null) return location;

            return new Location
            {
                Title = address
            };
        }
    }
}