using System.Collections.Generic;
using App.Data.DbContexts;
using App.Data.Entities.Settings;
using App.Data.Repositories;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace Delivery.Data.Repositories
{

    [Repository]
    public class ServerSettingRepository : DataRepository<TakeawayContext, ServerSetting, int>, IServerSettingRepository
    {
        public ServerSettingRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<ServerSetting> GetAllNonServer()
        {
            return GetMany(r => !r.IsServer.HasValue || !r.IsServer.Value);
        }
    }
}