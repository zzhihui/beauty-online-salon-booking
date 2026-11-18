using System.Collections.Generic;
using System.Linq;
using App.BusinessEngine.BusinessEngine;
using App.Data.Entities.Authentication;
using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;

namespace App.Service.Services
{
    public interface IShopUserServiceHandler
    {
        WsShopUser LoginShopAgent(string username, string password, int shopId);
        int SaveShopUser(WsShopUser newShopUser, int shopId);
        int DeleteShopUser(int id, int shopId);

        List<WsShopServiceUser> GetShopServiceUser(int shopId);

        WsShopUser GetShopUserById(int userId, int shopId);
        WsShopUser RemoveShopUseById(int userId, int shopId);
        List<WsBooking> GetAllBookings(int shopId);
        List<WsShopCustomer> GetAllCustomers(int shopId);
        string UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus);
        string UpdateUserCredit(int customerId, int shopId, int credits);
        string SendUserVoucher(int customerId, int shopId, int voucherId);
        List<WsContactForm> GetUserFeedback(int shopId);
        List<WsVoucherType> GetAllVoucherTypes(int shopId);
        List<WsShopService> GetAllShopService(int shopId);
        WsShopCustomer GetCustomerById(int customerId, int shopId);
        int SaveCustomer(WsShopCustomer customer, int shopId);
        int SaveVoucherType(WsVoucherType voucher, int shopId);

        int SaveShopService(WsShopService shopService, int shopId);

        WsVoucherType GetVoucherTypeById(int id, int shopId);

        WsShopService GetShopServiceById(int id, int shopId);

        int DeleteVoucherType(int id, int shopId);

        int DeleteShopService(int id, int shopId);

        List<WsShopUser> GetAllShopUser(int shopId);
    }

    [ServiceHandler]
    public class ShopUserServiceHandler : ServiceBase, IShopUserServiceHandler
    {
        private readonly IShopBusinessEngine _shopBusinessEngine;

        public ShopUserServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer, IShopBusinessEngine shopBusinessEngine)
            : base(validatorFactory, unitOfWork, transformer)
        {
            _shopBusinessEngine = shopBusinessEngine;
        }

        public int DeleteVoucherType(int id, int shopId)
        {
            return Query(() =>_shopBusinessEngine.DeleteVoucherType(id, shopId));
        }

        public int DeleteShopService(int id, int shopId)
        {
            return Query(() => _shopBusinessEngine.DeleteShopService(id, shopId));
        }

        public WsShopService GetShopServiceById(int id, int shopId)
        {
            return Query(() =>
            {
                var shopService = _shopBusinessEngine.GetShopServiceById(id, shopId);

                if (shopService == null)
                    throw new System.Exception("Voucher type not exist!");

                return Transformer.Transform<ShopService, WsShopService>(shopService);
            });
        }

        public WsVoucherType GetVoucherTypeById(int id, int shopId)
        {
            return Query(() =>
            {
                var voucherType = _shopBusinessEngine.GetVoucherTypeById(id, shopId);

                if (voucherType == null)
                    throw new System.Exception("Voucher type not exist!");

                return Transformer.Transform<VoucherType, WsVoucherType>(voucherType);
            });
        }

        public WsShopCustomer GetCustomerById(int customerId, int shopId)
        {
            return Query(() =>
            {
                var customer = _shopBusinessEngine.GetCustomerById(customerId, shopId);

                if (customer == null)
                    throw new System.Exception("Customer not exist!");

                return Transformer.Transform<ShopCustomer, WsShopCustomer>(customer);
            });
        }

        public int SaveShopService(WsShopService shopService, int shopId)
        {
            return Query(() =>
            {
                var transformIn = Transformer.Transform<WsShopService, ShopService>(shopService);

                var result = _shopBusinessEngine.SaveShopService(transformIn, shopId);

                return 1;
            });
        }

        public int SaveVoucherType(WsVoucherType voucher, int shopId)
        {
            return Query(() =>
            {
                var transformIn = Transformer.Transform<WsVoucherType, VoucherType>(voucher);

                var result = _shopBusinessEngine.SaveVoucherType(transformIn, shopId);

                return 1;
            });
        }

        public int SaveCustomer(WsShopCustomer customer, int shopId)
        {
            return Query(() =>
            {
                var transformIn = Transformer.Transform<WsShopCustomer, ShopCustomer>(customer);

                var result = _shopBusinessEngine.SaveCustomer(transformIn, shopId);

                return 1;
            });
        }

        public List<WsShopUser> GetAllShopUser(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var shopUser = _shopBusinessEngine.GetAllShopUser(shopId);

                return shopUser?.Select(service => new WsShopUser
                {
                    ShopId = service.ShopId,
                    Id = service.Id,
                    DisplayName = service.DisplayName,
                    Email = service.Email,
                    IsActive = service.IsActive,
                    Password = service.Password,
                    Pin = service.Pin,
                    Updated = service.Updated,
                    UserGroupId = service.UserGroupId,
                    UserName = service.UserName,
                    Created = service.Created
                }).ToList();
            });
        }

        public List<WsShopServiceUser> GetShopServiceUser(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var shopServiceUsers = _shopBusinessEngine.GetShopServiceUser(shopId);

                return shopServiceUsers?.Select(serviceUser => new WsShopServiceUser
                {
                    ShopId = shopId,
                    Id = serviceUser.Id,
                    ServiceId = serviceUser.ServiceId,
                    UserId = serviceUser.UserId
                }).ToList();
            });
        }

        public List<WsShopService> GetAllShopService(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var serviceInfo = _shopBusinessEngine.GetShopService(shopId);

                //return Transformer.Transform<List<ShopService>, List<WsShopService>>(serviceInfo);
                //return serviceInfo.Select(service => Transformer.Transform<ShopService, WsShopService>(service)).ToList();

                return serviceInfo?.Select(service => new WsShopService
                {
                    ShopId = service.ShopId,
                    Id = service.Id,
                    Name = service.Name,
                    Category = service.Category,
                    Description = service.Description,
                    Length = service.Length,
                    MaxPerDay = service.MaxPerDay,
                    PriceFrom = service.PriceFrom,
                    PriceTo = service.PriceTo
                }).ToList();
            });
        }

        public List<WsVoucherType> GetAllVoucherTypes(int shopId)
        {
            return Query(() =>
            {
                var voucherTypes = _shopBusinessEngine.GetAllVoucherTypes(shopId);

                return voucherTypes?.Select(voucherType => new WsVoucherType
                {
                    ShopId = voucherType.ShopId,
                    Created = voucherType.Created,
                    Id = voucherType.Id,
                    Name = voucherType.Name,
                    Desc = voucherType.Desc,
                    ServiceId = voucherType.ServiceId
                }).OrderByDescending(x => x.Created).ToList();
            });
        }

        public List<WsContactForm> GetUserFeedback(int shopId)
        {
            return Query(() =>
            {
                var userFeedback = _shopBusinessEngine.GetUserFeedback(shopId);

                return userFeedback?.Select(feedback => new WsContactForm
                {
                    ShopId = feedback.ShopId,
                    Created = feedback.Created,
                    Email = feedback.Email,
                    Message = feedback.Message,
                    Id = feedback.Id,
                    Name = feedback.Name,
                    Subject = feedback.Subject,
                    Mobile = feedback.Mobile
                }).OrderByDescending(x => x.Created).ToList();
            });
        }

        public string UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus)
        {
            return Query(() => _shopBusinessEngine.UpdateUserBookingStatus(bookingId, shopId, bookingStatus));
        }

        public string SendUserVoucher(int customerId, int shopId, int voucherId)
        {
            return Query(() => _shopBusinessEngine.SendUserVoucher(customerId, shopId, voucherId));
        }

        public string UpdateUserCredit(int customerId, int shopId, int credits)
        {
            return Query(() => _shopBusinessEngine.UpdateUserCredit(customerId, shopId, credits));
        }

        public List<WsBooking> GetAllBookings(int shopId)
        {
            return Query(() =>
            {
                var bookingHistory = _shopBusinessEngine.GetAllBookings(shopId);

                return bookingHistory?.Select(booking => new WsBooking
                {
                    ShopId = booking.ShopId,
                    Id = booking.Id,
                    DateTimeSlot = booking.DateTimeSlot,
                    Created = booking.Created,
                    CustomerId = booking.CustomerId,
                    ServiceId = booking.ServiceId,
                    ServiceName = _shopBusinessEngine.GetServiceName(booking.ServiceId),
                    Status = booking.Status,
                    Ref = booking.Ref
                }).OrderByDescending(x => x.DateTimeSlot).ToList();
            });
        }

        public List<WsShopCustomer> GetAllCustomers(int shopId)
        {
            return Query(() =>
            {
                var customers = _shopBusinessEngine.GetAllCustomers(shopId);

                return customers?.Select(booking => new WsShopCustomer
                {
                    ShopId = booking.ShopId,
                    Id = booking.Id,
                    Created = booking.Created,
                    LastName = booking.LastName,
                    Address = booking.Address,
                    FirstName = booking.FirstName,
                    IsActive = booking.IsActive,
                    Password = booking.Password,
                    Email = booking.Email,
                    PhoneNumber = booking.PhoneNumber,
                    Updated = booking.Updated,
                    StoreCredit = booking.StoreCredit,
                    ContactName = booking.ContactName,
                    MobileNumber = booking.PhoneNumber
                }).OrderByDescending(x => x.FirstName).ToList();
            });
        }

        public WsShopUser LoginShopAgent(string username, string password, int shopId)
        {
            return Query(() =>
            {
                var shopUser = _shopBusinessEngine.LoginShopAgent(username, password, shopId);
                if (shopUser == null)
                    throw new System.Exception("Invalid user!");

                return Transformer.Transform<ShopUser, WsShopUser>(shopUser);
            });
        }
        

        public int SaveShopUser(WsShopUser shopUser, int shopId)
        {
            return Query(() =>
            {
                var transformIn = Transformer.Transform<WsShopUser, ShopUser>(shopUser);

                var result = _shopBusinessEngine.SaveShopUser(transformIn, shopId);

                return 1;
            });
        }

        public int DeleteShopUser(int id, int shopId)
        {
            return Query(() => _shopBusinessEngine.DeleteShopUser(id, shopId));
        }

        public WsShopUser GetShopUserById(int id, int shopId)
        {
            return Query(() =>
            {
                var shopUser = _shopBusinessEngine.GetShopUserById(id, shopId);

                if (shopUser == null)
                    throw new System.Exception("Customer not exist!");

                return Transformer.Transform<ShopUser, WsShopUser>(shopUser);
            });
        }

        public WsShopUser RemoveShopUseById(int userId, int shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}