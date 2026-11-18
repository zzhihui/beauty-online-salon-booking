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
    public interface IBookingServiceHandler {

    }
    [ServiceHandler]
    public class BookingServiceHandler : ServiceBase, IBookingServiceHandler
    {
        private readonly IBookingBusinessEngine _bookingBusinessEngine;
        private readonly ITokenEncryptor _tokenEncryptor;

        public BookingServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, 
            ITransformer transformer, IBookingBusinessEngine bookingBusinessEngine, 
            ITokenEncryptor tokenEncryptor) : base(validatorFactory, unitOfWork, transformer)
        {
            _bookingBusinessEngine = bookingBusinessEngine;
            _tokenEncryptor = tokenEncryptor;
        }

    }
}
