using System.Linq;
using App.Data.DbContexts;
using App.Data.Entities.Shops;
using App.Data.Repositories;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace Delivery.Data.Repositories
{
    [Repository]
    public class ShopInfoRepository : DataRepository<TakeawayContext, ShopInfo, int>, IShopInfoRepository
    {
        public ShopInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ShopInfo GetShopInfo()
        {
            return GetAll().FirstOrDefault();
        }
    }
}