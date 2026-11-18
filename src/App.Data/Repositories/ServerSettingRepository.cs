using App.Data.DbContexts;
using App.Data.Entities.Settings;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{
    public interface IServerSettingRepository : IDataRepository<ServerSetting, int>
    {
    
    }
    [Repository]
    public class ServerSettingRepository : DataRepository<AppDbContext, ServerSetting, int>, IServerSettingRepository
    {
        public ServerSettingRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}