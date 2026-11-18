using App.Data.DbContexts;
using App.Data.Entities.Health;
using App.Data.Repositories;
using Core.Common.Core;

namespace Delivery.Data.Repositories
{
    public class SyncStatusRepository : DataRepository<TakeawayContext, SyncStatus, int>, ISyncStatusRepository
    {
        public SyncStatusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}