using System.Collections.Generic;
using System.Linq;
using App.Data.DbContexts;
using App.Data.Entities.Shops;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{
    public interface IShopNewsRepository : IDataRepository<ShopNews, int>
    {
        List<ShopNews> GetLatestNews(int shopId);
    }


    [Repository]
    public class ShopNewsRepository : DataRepository<AppDbContext, ShopNews, int>, IShopNewsRepository
    {
        public ShopNewsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<ShopNews> GetLatestNews(int shopId)
        {
            return GetMany(r => r.ShopId == shopId).OrderByDescending(s => s.Id).Take(5).ToList();
        }
    }

}