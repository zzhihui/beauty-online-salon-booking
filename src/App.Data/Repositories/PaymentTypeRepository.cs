using System.Collections.Generic;
using App.Data.DbContexts;
using App.Data.Entities.Shops;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{
    public interface IPaymentTypeRepository : IDataRepository<PaymentType, int>
    {
       
    }

    [Repository]
    public class PaymentTypeRepository : DataRepository<AppDbContext, PaymentType, int>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }        
    }
}