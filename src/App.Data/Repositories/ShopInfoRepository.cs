using App.Data.DbContexts;
using App.Data.Entities.Shops;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{

    public interface IShopInfoRepository : IDataRepository<ShopInfo, int>
    {
        ShopInfo GetShopInfo(int shopId);
    }

    [Repository]
    public class ShopInfoRepository : DataRepository<AppDbContext, ShopInfo, int>, IShopInfoRepository
    {
        public ShopInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        
        public ShopInfo GetShopInfo(int shopId)
        {
            return GetOne(r => r.ShopId == shopId && r.Isactive.HasValue && r.Isactive.Value);
        }
    }
}