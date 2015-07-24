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

        private readonly ILocationRepository _locationRepository;

        protected SharedService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
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