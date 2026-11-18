using System;
using System.Collections.Generic;
using System.Configuration;
using App.BusinessEngine.Exception;
using App.BusinessEngine.MangoDb;
using App.BusinessEngine.Utility;
using App.Data.Entities.Authentication;
using App.Data.Entities.Settings;
using App.Data.Entities.Shops;
using App.Data.Repositories;
using Core.Common.Utility;

namespace App.BusinessEngine.BusinessEngine
{
    public interface ICustomerBusinessEngine
    {
        ShopCustomer LoginCustomer(ShopCustomer requestTransIn, int shopId);
        ShopCustomer AddCustomer(ShopCustomer requestTransIn, int shopId);
        ShopCustomer UpdateCustomer(ShopCustomer requestTransIn, int shopId);
        bool SendCustomerPassword(string email, int shopId);
        bool SaveContactForm(ContactForm contactForm, int shopId);
        ShopCustomer GetCustomerById(int id, int shopId);
        List<Booking> GetBookingHistory(int customerId, int shopId);
        string GetServiceName(int serviceId);
        bool CancelBooking(int customerId, int shopId, int bookingId);
    }

    public class CustomerBusinessEngine:BusinessEngineBase, ICustomerBusinessEngine
    {

        private readonly IShopCustomerRepository _shopCustomerRepository;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IEmailSender _emailSender;
        private readonly IMangoDbContext _mangoDbContext;

        public CustomerBusinessEngine(IShopCustomerRepository shopCustomerRepository, IEncryptionHelper encryptionHelper,
            IMangoDbContext mangoDbContext, IEmailSender emailSender)
        {
            _shopCustomerRepository = shopCustomerRepository;
            _encryptionHelper = encryptionHelper;
            _mangoDbContext = mangoDbContext;
            _emailSender = emailSender;
        }

        public bool SaveContactForm(ContactForm contactForm, int shopId)
        {
            //TODO: Contact form repository.
            return Execute(() => _mangoDbContext.SaveContactForm(contactForm, shopId));
        }

        public bool CancelBooking(int customerId, int shopId, int bookingId)
        {
            return Execute(() => _mangoDbContext.CancelBooking(customerId, shopId, bookingId));
        }

        public List<Booking> GetBookingHistory(int customerId, int shopId)
        {
            return Execute(() => _mangoDbContext.GetAllBookingsByCustomerId(customerId, shopId));
        }

        public string GetServiceName(int serviceId)
        {
            return Execute(() => _mangoDbContext.GetServiceName(serviceId));
        }

        public bool SendCustomerPassword(string email, int shopId)
        {
            return Execute(() =>
            {
                //Get customer
                var customer = _mangoDbContext.GetCustomerByEmailAddress(email, shopId);

                if (customer == null)
                    throw new BusinessEngineException(new System.Exception("No Customer Exsits"));

                //Decrypt password
                var clearPassword = _encryptionHelper.DecriptEncryptXml(customer.Password);

                //Send email
                return _emailSender.SendPlainEmail(email,
                    ConfigurationManager.AppSettings["ForgotPasswordSubject"], $"Your password is {clearPassword}");
            });
        }

        private EmailServerSettings GetEmailServerSettings()
        {
            bool smtpEnabled;
            bool.TryParse(ConfigurationManager.AppSettings["SmtpEnabled"], out smtpEnabled);
            return new EmailServerSettings
            {
                MailFrom = ConfigurationManager.AppSettings["SmtpFromEmail"],
                SmtpEnabled = smtpEnabled,
                SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"],
                SmtpServer = ConfigurationManager.AppSettings["SmtpServer"],
                SmtpUserName = ConfigurationManager.AppSettings["SmtpUserName"]
            };
        }

        public ShopCustomer LoginCustomer(ShopCustomer requestTransIn, int shopId)
        {
            return Execute(() =>
            {
                var encryptedPassword = _encryptionHelper.EncryptXml(requestTransIn.Password);

                //var customer = _shopCustomerRepository.GetCustomerByEmailAndPassword(requestTransIn.Email, encrptedPassword, shopId);
                var customer = _mangoDbContext.GetCustomerByEmailPassword(requestTransIn.Email, encryptedPassword, shopId);

                if (customer == null)
                    throw new BusinessEngineException(new System.Exception("No Customer Exists"));

                //Clear password
                customer.Password = "";
                customer.TokenExpire = DateTime.Now.AddDays(1).AddHours(1);

                return customer;
            });
        }

        public ShopCustomer GetCustomerById(int id, int shopId)
        {
            return Execute(() =>
            {
                //TODO: GET Customer by id.
                var customer = _mangoDbContext.GetCustomerById(id, shopId);

                if (customer == null)
                    throw new BusinessEngineException(new System.Exception("No Customer Exists"));

                //Clear password
                customer.Password = "";
                customer.TokenExpire = DateTime.Now.AddDays(1).AddHours(1);

                return customer;
            });
        }

        public ShopCustomer AddCustomer(ShopCustomer requestTransIn, int shopId)
        {
            return Execute(() =>
            {
                requestTransIn.Email = requestTransIn.Email.ToLower();
                //var existingCustomer = _shopCustomerRepository.GetById(requestTransIn.Id, shopId);
                var existingCustomer = _mangoDbContext.GetCustomerByEmailAddress(requestTransIn.Email, shopId);
                if (existingCustomer != null)
                    throw new System.Exception("Email already registered!");

                var encryptedPassword = _encryptionHelper.EncryptXml(requestTransIn.Password);
                requestTransIn.Password = encryptedPassword;
                requestTransIn.Created=DateTime.Now;
                requestTransIn.Updated = DateTime.Now;
                requestTransIn.ShopId = shopId;

                //_shopCustomerRepository.Add(requestTransIn);
                _mangoDbContext.CreateCustomer(requestTransIn, shopId);

                return requestTransIn;
            });
        }

        public ShopCustomer UpdateCustomer(ShopCustomer requestTransIn, int shopId)
        {
            return Execute(() =>
            {
                //var existingCustomer = _shopCustomerRepository.GetById(requestTransIn.Id, shopId);

                var existingCustomer = _mangoDbContext.GetCustomerById(requestTransIn.Id, shopId);

                if (existingCustomer==null)
                    throw new BusinessEngineException(new System.Exception("Cannot find existing customer"));

                if (!string.IsNullOrEmpty(requestTransIn.OldPassword))
                {
                    requestTransIn.OldPassword = _encryptionHelper.EncryptXml(requestTransIn.OldPassword);
                    if (existingCustomer.Password != requestTransIn.OldPassword)
                    {
                        throw new BusinessEngineException(new System.Exception("Incorrect current password"));
                    }
                    
                    existingCustomer.Password = _encryptionHelper.EncryptXml(requestTransIn.Password);
                }                  

                existingCustomer.PhoneNumber = requestTransIn.PhoneNumber;
                existingCustomer.Email = requestTransIn.Email;
                existingCustomer.Address = requestTransIn.Address;
                existingCustomer.IsActive = requestTransIn.IsActive;

                existingCustomer.Updated = DateTime.Now;
                existingCustomer.ShopId = shopId;
                existingCustomer.FirstName = requestTransIn.FirstName;
                existingCustomer.LastName = requestTransIn.LastName;

                _mangoDbContext.UpdateCustomer(existingCustomer, shopId);

                return existingCustomer;
            });
        }
    }
}
