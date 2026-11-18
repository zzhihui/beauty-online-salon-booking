using System;
using System.Collections.Generic;
using System.Linq;
using App.BusinessEngine.BusinessEngine;
using App.Data.Entities.Authentication;
using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Service.Transformers;
using App.Service.Validations;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility;
using FluentValidation;
using Newtonsoft.Json;

namespace App.Service.Services
{
    public interface ICustomerServiceHandler
    {
        WsShopCustomer LoginCustomer(string email, string password, int shopId);
        WsShopCustomer RegisterCustomer(WsShopCustomer customer, int shopId);
        WsShopCustomer AddCustomer(WsShopCustomer customer, int shopId);
        WsShopCustomer UpdateCustomer(WsShopCustomer customer, int shopId);
        WsShopCustomer GetAllCustomers(int shopId);
        WsShopCustomer GetCustomerById(int id, int shopId);
        WsShopCustomer RemoveCustomerById(int customerId, int shopId);
        bool SendRecoveryEmail(string email, int shopId);
        bool SaveContactForm(WsContactForm contactForm, int shopId);
        List<WsBooking> GetBookingHistory(int customerId, int shopId);
        bool CancelBooking(int customerId, int shopId, int bookingId);
    }

    [ServiceHandler]
    public class CustomerServiceHandler : ServiceBase, ICustomerServiceHandler
    {
        private readonly ICustomerBusinessEngine _customerBusinessEngine;
        private readonly ITokenEncryptor _tokenEncryptor;
        
        public CustomerServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer, ICustomerBusinessEngine customerBusinessEngine, ITokenEncryptor tokenEncryptor) : base(validatorFactory, unitOfWork, transformer)
        {
            _customerBusinessEngine = customerBusinessEngine;
            _tokenEncryptor = tokenEncryptor;
        }

        public bool SendRecoveryEmail(string email, int shopId)
        {
            return Execute(() => _customerBusinessEngine.SendCustomerPassword(email, shopId));
        }

        public bool SaveContactForm(WsContactForm contactForm, int shopId)
        {
            var contactInfo = Transformer.Transform<WsContactForm, ContactForm>(contactForm);
            
            return Execute(() => _customerBusinessEngine.SaveContactForm(contactInfo, shopId));
        }

        public bool CancelBooking(int customerId, int shopId, int bookingId)
        {
            return Query(
                () => _customerBusinessEngine.CancelBooking(customerId, shopId, bookingId)
            );
        }

        public WsShopCustomer LoginCustomer(string email, string password, int shopId)
        {
            return Query(() =>
            {
                //Validate Request
                var request = new WsShopCustomer { Email = email, Password = password };
                Validate(request, WsShopCustomerEnum.LoginCustomer.ToString());

                //Transform in 
                var requestTransIn = Transformer.Transform<WsShopCustomer, ShopCustomer>(request);

                //Action
                var shopCustomerPoco = _customerBusinessEngine.LoginCustomer(requestTransIn, shopId);
                if (shopCustomerPoco == null)
                    return null;

                //Transform out 
                var customer= Transformer.Transform<ShopCustomer, WsShopCustomer>(shopCustomerPoco);

                customer.ClientToken = _tokenEncryptor.Encrypt(JsonConvert.SerializeObject(customer));

                return customer;
            });
        }

        public List<WsBooking> GetBookingHistory(int customerId, int shopId)
        {
            return Execute(() =>
            {
                var bookingHistory = _customerBusinessEngine.GetBookingHistory(customerId, shopId);

                return bookingHistory?.Select(booking => new WsBooking
                {
                    ShopId = booking.ShopId,
                    Id = booking.Id,
                    DateTimeSlot = booking.DateTimeSlot,
                    Created = booking.Created,
                    CustomerId = booking.CustomerId,
                    ServiceId = booking.ServiceId,
                    ServiceName = _customerBusinessEngine.GetServiceName(booking.ServiceId),
                    Status = booking.Status,
                    Ref = booking.Ref
                }).OrderByDescending(x => x.DateTimeSlot).ToList();
            });
        }
        
        public WsShopCustomer RegisterCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {
                //Validate Request
                Validate(customer, WsShopCustomerEnum.RegisterCustomer.ToString());

                //Transform in 
                var requestTransIn = Transformer.Transform<WsShopCustomer, ShopCustomer>(customer);

                //Action
                var shopCustomerPoco = _customerBusinessEngine.AddCustomer(requestTransIn, shopId);
                if (shopCustomerPoco == null)
                    return null;
                
                //Transform out 
                return Transformer.Transform<ShopCustomer, WsShopCustomer>(shopCustomerPoco);
            });
        }

        public WsShopCustomer AddCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {
                //Validate Request
                Validate(customer, WsShopCustomerEnum.RegisterCustomer.ToString());

                //Transform in 
                var requestTransIn = Transformer.Transform<WsShopCustomer, ShopCustomer>(customer);

                //Action
                var shopCustomerPoco = _customerBusinessEngine.AddCustomer(requestTransIn, shopId);
                if (shopCustomerPoco == null)
                    return null;

                //Transform out 
                return Transformer.Transform<ShopCustomer, WsShopCustomer>(shopCustomerPoco);
            });
        }

        public WsShopCustomer UpdateCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {
                //Validate Request
                Validate(customer, WsShopCustomerEnum.UpdateCustomer.ToString());

                //Transform in 
                var requestTransIn = Transformer.Transform<WsShopCustomer, ShopCustomer>(customer);

                //Action
                var shopCustomerPoco = _customerBusinessEngine.UpdateCustomer(requestTransIn, shopId);
                if (shopCustomerPoco == null)
                    return null;

                //Transform out 
                return Transformer.Transform<ShopCustomer, WsShopCustomer>(shopCustomerPoco);
            });
        }

        public WsShopCustomer GetAllCustomers(int shopId)
        {
            throw new NotImplementedException();
        }

        public WsShopCustomer GetCustomerById(int id, int shopId)
        {
            return Query(() =>
            {
                //Action
                var shopCustomerPoco = _customerBusinessEngine.GetCustomerById(id, shopId);
                if (shopCustomerPoco == null)
                    return null;

                //Transform out 
                var customer = Transformer.Transform<ShopCustomer, WsShopCustomer>(shopCustomerPoco);

                customer.ClientToken = _tokenEncryptor.Encrypt(JsonConvert.SerializeObject(customer));

                return customer;
            });
        }

        public WsShopCustomer RemoveCustomerById(int customerId, int shopId)
        {
            throw new NotImplementedException();
        }
    }
}