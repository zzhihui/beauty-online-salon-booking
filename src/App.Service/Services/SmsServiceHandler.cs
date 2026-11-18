using System;
using App.BusinessEngine.BusinessEngine;
using App.Data.Entities.Authentication;
using App.Service.Contract.Entities.Authentication;
using App.Service.Transformers;
using App.Service.Validations;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility;
using FluentValidation;
using Newtonsoft.Json;

namespace App.Service.Services
{
    public interface ISmsServiceHandler
    {

    }

    [ServiceHandler]
    public class SmsServiceHandler : ServiceBase, ISmsServiceHandler
    {
        private readonly ICustomerBusinessEngine _customerBusinessEngine;
        private readonly ITokenEncryptor _tokenEncryptor;

        public SmsServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer, ICustomerBusinessEngine customerBusinessEngine, ITokenEncryptor tokenEncryptor) : base(validatorFactory, unitOfWork, transformer)
        {
            _customerBusinessEngine = customerBusinessEngine;
            _tokenEncryptor = tokenEncryptor;
        }
    }
}
