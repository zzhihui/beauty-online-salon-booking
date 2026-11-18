using App.Data.DbContexts;
using App.Data.Entities.Authentication;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{
    public interface IShopUserRepository : IDataRepository<ShopUser, int>
    {
       
    }

    [Repository]
    public class ShopUserRepository : DataRepository<AppDbContext, ShopUser, int>, IShopUserRepository
    {
        public ShopUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}