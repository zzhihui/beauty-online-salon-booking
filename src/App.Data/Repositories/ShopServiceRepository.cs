using App.Data.DbContexts;
using App.Data.Entities.Shops;
using Core.Common.Core;

namespace App.Data.Repositories
{
    public interface IShopServiceRepository : IDataRepository<ShopService, int>
    {

    }

    public class ShopServiceRepository: DataRepository<AppDbContext, ShopService, int>, IShopServiceRepository
    {
        public ShopServiceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
