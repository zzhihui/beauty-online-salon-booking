using App.Service.Contract.Entities.Authentication;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;



namespace App.Service.Services
{
    public interface IhelpServiceHandler1
    {



    }

    [ServiceHandler]
    public class helpServiceHandler1 : ServiceBase, IhelpServiceHandler1
    {
        public helpServiceHandler1(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer)
            : base(validatorFactory, unitOfWork, transformer)
        {
        }

    }
}