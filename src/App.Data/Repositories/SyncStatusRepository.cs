using App.Data.DbContexts;
using App.Data.Entities.Health;
using App.Data.Entities.Shops;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;

namespace App.Data.Repositories
{
    public interface ISyncStatusRepository : IDataRepository<SyncStatus, int>
    {
    }


    [Repository]
    public class SyncStatusRepository : DataRepository<AppDbContext, SyncStatus, int>, ISyncStatusRepository
    {
        public SyncStatusRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

    }

}