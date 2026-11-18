using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Utility;
using App.Web.ViewModels;
using Newtonsoft.Json;

namespace App.Web.Application.ServiceClient
{

    public interface IShopUserServiceClient {
        RestResponse<WsShopUser> LoginShopAgent(string email, string password, int shopId);

        RestResponse<List<WsBooking>> GetAllBookings(int shopId);

        RestResponse<List<WsShopCustomer>> GetAllCustomers(int shopId);
        RestResponse<int> SaveCustomer(WsShopCustomer customer, int shopId);
        
        RestResponse<string> SendUserVoucher(int customerId, int shopId, int voucherId);
        
        RestResponse<List<WsShopUser>> GetAllShopUser(int shopId);
        RestResponse<WsShopUser> GetShopUserById(int id, int shopId);
        RestResponse<int> SaveShopUser(WsShopUser shopUser, int shopId);
        RestResponse<int> DeleteShopUser(int id, int shopId);

        //ShopServiceUser
        RestResponse<List<WsShopServiceUser>> GetShopServiceUser(int shopId);

        //VoucherType CRUD
        RestResponse<WsVoucherType> GetVoucherTypeById(int id, int shopId);
        RestResponse<List<WsVoucherType>> GetAllVoucherTypes(int shopId);
        RestResponse<int> SaveVoucherType(WsVoucherType voucher, int shopId);
        RestResponse<int> DeleteVoucherType(int id, int shopId);

        //ShopService CRUD
        RestResponse<int> SaveShopService(WsShopService shopService, int shopId);
        RestResponse<List<WsShopService>> GetAllShopService(int shopId);
        RestResponse<WsShopService> GetShopServiceById(int id, int shopId);
        RestResponse<int> DeleteShopService(int id, int shopId);
        

        RestResponse<string> UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus);
        RestResponse<string> UpdateUserCredit(int customerId, int shopId, int credits);
        

        RestResponse<List<WsContactForm>> GetUserFeedback(int shopId);
        RestResponse<WsShopCustomer> GetCustomerById(int customerId, int shopId);

        //schedules
        RestResponse<List<WsSchedule>> GetAllSchedules(int shopId);

        RestResponse<int> SaveAppointment(WsSchedule appointment, int shopId);
        RestResponse<int> DeleteAppointment(WsSchedule appointment, int shopId);
        RestResponse<int> AddAppointment(WsSchedule appointment, int shopId);
    }
    
    public class ShopUserServiceClient: RestServiceBase, IShopUserServiceClient
    {
        public RestResponse<WsShopUser> LoginShopAgent(string email, string password, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/LoginShopAgent?email={email}&password={password}&shopId={shopId}");
                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopUser>(responseJson);
            });
        }


        public RestResponse<int> DeleteShopService(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/DeleteShopService?id={id}&shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<int> DeleteShopUser(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/DeleteShopUser?id={id}&shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<int> DeleteVoucherType(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/DeleteVoucherType?id={id}&shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<int> SaveShopService(WsShopService shopService, int shopId)
        {
            return Execute(() =>
            {
                var response = PostService($"{Constants.SiteUrl}/api/ShopUserApi/SaveShopService?shopId={shopId}", new StringContent(JsonConvert.SerializeObject(shopService), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<WsShopService> GetShopServiceById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetShopServiceById?id={id}&shopId={shopId}");
                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopService>(responseJson);
            });
        }


        public RestResponse<List<WsShopUser>> GetAllShopUser(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetAllShopUser?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopUser>>(responseJson);
            });
        }

        public RestResponse<List<WsShopService>> GetAllShopService(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetAllShopService?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopService>>(responseJson);
            });
        }

        public RestResponse<int> SaveShopUser(WsShopUser shopUser, int shopId)
        {
            return Execute(() =>
            {
                var response = PostService($"{Constants.SiteUrl}/api/ShopUserApi/SaveShopUser?shopId={shopId}", new StringContent(JsonConvert.SerializeObject(shopUser), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<int> SaveVoucherType(WsVoucherType voucher, int shopId)
        {
            return Execute(() =>
            {
                var response = PostService($"{Constants.SiteUrl}/api/ShopUserApi/SaveVoucherType?shopId={shopId}", new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<WsShopUser> GetShopUserById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetShopUserById?id={id}&shopId={shopId}");
                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopUser>(responseJson);
            });
        }

        public RestResponse<WsVoucherType> GetVoucherTypeById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetVoucherTypeById?id={id}&shopId={shopId}");
                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsVoucherType>(responseJson);
            });
        }

        public RestResponse<int> SaveCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {
                var response = PostService($"{Constants.SiteUrl}/api/ShopUserApi/SaveCustomer?shopId={shopId}", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<WsShopCustomer> GetCustomerById(int customerId, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetCustomerById?shopId={shopId}&customerId={customerId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopCustomer>(responseJson);
            });
        }

        public RestResponse<List<WsShopServiceUser>> GetShopServiceUser(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetShopServiceUser?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopServiceUser>>(responseJson);
            });
        }

        public RestResponse<List<WsVoucherType>> GetAllVoucherTypes(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetAllVoucherTypes?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsVoucherType>>(responseJson);
            });
        }

        public RestResponse<List<WsContactForm>> GetUserFeedback(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetUserFeedback?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsContactForm>>(responseJson);
            });
        }

        public RestResponse<string> SendUserVoucher(int customerId, int shopId, int voucherId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/SendUserVoucher?shopId={shopId}&customerId={customerId}&voucherId={voucherId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<string>(responseJson);
            });
        }

        public RestResponse<string> UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/UpdateUserBookingStatus?shopId={shopId}&bookingId={bookingId}&bookingStatus={bookingStatus}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<string>(responseJson);
            });
        }

        public RestResponse<string> UpdateUserCredit(int customerId, int shopId, int credits)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/UpdateUserCredit?shopId={shopId}&customerId={customerId}&credits={credits}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<string>(responseJson);
            });
        }

        public RestResponse<List<WsShopCustomer>> GetAllCustomers(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetAllCustomers?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopCustomer>>(responseJson);
            });
        }

        public RestResponse<List<WsBooking>> GetAllBookings(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopUserApi/GetAllBookings?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsBooking>>(responseJson);
            });
        }

        public RestResponse<List<WsSchedule>> GetAllSchedules(int shopId)
        {
            //problems when passing datetime as parameters.
            return Execute(() =>
            {
                var response = GetService($"{Constants.SiteUrl}/api/ShopApi/GetSchedules?shopId={shopId}");

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsSchedule>>(responseJson);
            });
        }

        public RestResponse<int> SaveAppointment(WsSchedule appointment, int shopId)
        {
            return Execute(() =>
            {
                var response = PostService($"{Constants.SiteUrl}/api/ShopApi/SaveAppointment?shopId={shopId}", 
                    new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<int> DeleteAppointment(WsSchedule appointment, int shopId)
        {
            return Execute(() =>
            {
                var response = PostService($"{Constants.SiteUrl}/api/ShopApi/DeleteAppointment?shopId={shopId}",
                    new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }

        public RestResponse<int> AddAppointment(WsSchedule appointment, int shopId)
        {
            return Execute(() =>
            {
                appointment.ShopId = shopId;

                var response = PostService($"{Constants.SiteUrl}/api/ShopApi/AddAppointment?shopId={shopId}",
                    new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json"));

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<int>(responseJson);
            });
        }
    }
}