using System.Collections.Generic;
using App.Data.DbContexts;
using App.Data.Entities.Authentication;
using App.Data.Repositories;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace Delivery.Data.Repositories
{


    [Repository]
    public class ShopUserRepository : DataRepository<TakeawayContext, ShopUser, int>, IShopUserRepository
    {
        public ShopUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ShopUser GetActiveShopUser(string userName, string encryptedPassword)
        {
            return
                GetOne(
                    r =>
                        r.UserName == userName && r.Password == encryptedPassword && r.Isactive.HasValue &&
                        r.Isactive.Value);
        }

        public ShopUser GetActiveShopUserByName(string userName)
        {
            return
                GetOne(
                    r =>
                        r.UserName == userName && r.Isactive.HasValue &&
                        r.Isactive.Value);
        }

        public ShopUser GetShopAdmin(string userName, string password)
        {
            return
               GetOne(
                   r =>
                       r.UserName == userName &&
                       r.Password == password &&
                       r.UserGroupId == 1 && //Administrator
                       r.Isactive.HasValue &&
                       r.Isactive.Value);
        }

        public ShopUser GetUserByPin(string pin)
        {
            return
               GetOne(
                   r =>
                       r.Pin == pin &&
                       r.Isactive.HasValue &&
                       r.Isactive.Value);
        }

        public List<ShopUser> GetAllActive()
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value);
        }
    }
}