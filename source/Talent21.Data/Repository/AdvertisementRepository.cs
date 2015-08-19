using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {

    }

    public class AdvertisementRepository : EfRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }
    }

}