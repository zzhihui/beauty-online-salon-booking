using App.Data.DbContexts;
using App.Data.Entities.Authentication;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{
    public interface IShopCustomerRepository: IDataRepository<ShopCustomer, int>
    {
        ShopCustomer GetCustomerByEmailAndPassword(string email, string password, int shopId);
        ShopCustomer GetById(int id, int shopId);
    }
    
    [Repository]
    public class ShopCustomerRepository: DataRepository<AppDbContext, ShopCustomer, int>, IShopCustomerRepository
    {
        public ShopCustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ShopCustomer GetCustomerByEmailAndPassword(string email, string password, int shopId)
        {
            return GetOne(r => r.Email == email && r.Password == password && r.ShopId == shopId  && r.IsActive.HasValue && r.IsActive.Value);
        }

        public ShopCustomer GetById(int id, int shopId)
        {
            return GetOne(r => r.Id == id && r.ShopId == shopId);
        }
    }
}