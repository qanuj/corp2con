using System;
using e10.Shared.Providers;
using Talent21.Data.Repository;

namespace Talent21.Service.Core
{
    public class CommonService : SharedService
    {
        public CommonService(ILocationRepository locationRepository, ITransactionRepository transactionRepository, SellingOptions sellingOptions, IUserProvider userProvider, IMemberRepository mmbeRepository) : 
            base(locationRepository, transactionRepository, sellingOptions, userProvider, mmbeRepository)
        {
        }
        public override void AddView(int id, string userAgent, string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}