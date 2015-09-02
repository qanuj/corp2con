using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
   public class PaymentRepository : EfRepository<Payment>, IPaymentRepository
    {
       public PaymentRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
       { 
       }
    }

    public interface IPaymentRepository : IRepository<Payment>
    {
    }
}


