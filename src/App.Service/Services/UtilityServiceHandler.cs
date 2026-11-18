using App.Service.Contract.Entities.Health;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;

namespace App.Service.Services
{
    public interface IUtilityServiceHandler
    {
        WsSyncStatus SyncStatus(int shopId);
    }

    [ServiceHandler]
    public class UtilityServiceHandler : ServiceBase, IUtilityServiceHandler
    {
        public UtilityServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer)
            : base(validatorFactory, unitOfWork, transformer)
        {
        }

        public WsSyncStatus SyncStatus(int shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}