using System.Collections.Generic;
using App.Data.DbContexts;
using App.Data.Entities.Licence;
using App.Data.Repositories;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace Delivery.Data.Repositories
{
     [Repository]
    public class LicenceInfoRepository : DataRepository<TakeawayContext, LicenceInfo, int>, ILicenceInfoRepository
    {
         public LicenceInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
         {
         }

         public List<LicenceInfo> GetLicence(string hwid)
         {
             return GetMany(r => r.SettingKey == hwid);
         }
    }
}