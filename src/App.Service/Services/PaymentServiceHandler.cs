using System;
using System.Collections.Generic;
using App.Service.Contract.Entities.Payment;
using App.Service.Contract.Entities.Shop;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;

namespace App.Service.Services
{
    public interface IPaymentServiceHandler
    {
        List<WsPaymentMonth> LoadPaymentMonths(int shopId);
        List<WsPaymentYear> LoadPaymentYears(int shopId);
        WsPaymentInfo TakePayment(WsPayment payment, int shopId);
        List<WsPaymentType> GetAllPaymentTypes(int shopId);
    }

    [ServiceHandler]
    public class PaymentServiceHandler : ServiceBase, IPaymentServiceHandler
    {
        public PaymentServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer) : base(validatorFactory, unitOfWork, transformer)
        {
        }
        public List<WsPaymentMonth> LoadPaymentMonths(int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsPaymentYear> LoadPaymentYears(int shopId)
        {
            throw new NotImplementedException();
        }

        public WsPaymentInfo TakePayment(WsPayment payment, int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsPaymentType> GetAllPaymentTypes(int shopId)
        {
            throw new NotImplementedException();
        }
    }
}   