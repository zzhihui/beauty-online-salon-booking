using System;
using System.Collections.Generic;
using System.Linq;
using App.Data.Entities.Authentication;
using App.Data.Entities.Enum;
using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace App.BusinessEngine.MangoDb
{
    public interface IMangoDbContext
    {
        void CreateCustomer(ShopCustomer shopCustomer, int shopId);
        ShopCustomer GetCustomerByEmailAddress(string emailAddress, int shopId);
        void UpdateCustomer(ShopCustomer shopCustomer, int shopId);
        ShopCustomer GetCustomerByEmailPassword(string email, string password, int shopId);
        bool SaveContactForm(ContactForm contactForm, int shopId);
        ShopCustomer GetCustomerById(int id, int shopId);
        List<ShopService> GetShopService(int shopId);

        List<ShopServiceUser> GetShopServiceUser(int shopId);

        List<ShopUser> GetAllShopUser(int shopId);

        string BookService(Booking request);
        List<Booking> GetAllBookingsByCustomerId(int customerId, int shopId);
        List<Booking> GetAllActiveBookingsForCustomer(int customerId, int shopId);
        string GetServiceName(int serviceId);
        //List<Booking> GetBookingHistory(int customerId, int shopId);
        bool CancelBooking(int customerId, int shopId, int bookingId);
        ShopUser LoginShopAgent(string username, string password, int shopId);
        List<Booking> GetAllBookingsByShopId(int shopId);
        List<ShopCustomer> GetAllCustomersByShopId(int shopId);
        string UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus);
        string UpdateUserCredit(int customerId, int shopId, int credits);
        string SendUserVoucher(int customerId, int shopId, int voucherId);
        List<ContactForm> GetUserFeedback(int shopId);
        List<VoucherType> GetAllVoucherTypes(int shopId);
        int SaveCustomer(ShopCustomer customer, int shopId);
        int SaveOrUpdateVoucherType(VoucherType voucher, int shopId);
        int SaveOrUpdateShopUser(ShopUser shopUser, int shopId);
        int SaveOrUpdateShopService(ShopService shopService, int shopId);
        VoucherType GetVoucherTypeById(int id, int shopId);
        ShopUser GetShopUserById(int id, int shopId);
        ShopService GetShopServiceById(int id, int shopId);
        int DeleteVoucherType(int id, int shopId);
        int DeleteShopService(int id, int shopId);
        int DeleteShopUser(int id, int shopId);
        int DeleteCustomerBookings(int customerId, int shopId);
        List<ShopUserScheduler> GetBookedSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId);
        ShopSettings GetShopSettings(int shopId);
        List<Booking> GetAllBookingsForShopUserOnDate(int shopId, int shopUserId, DateTime scheduledDate);
        bool AddSchedule(Booking requestTransIn);

        List<ShopUserScheduler> GetAllBookedSlotsForScheduler(int shopId, DateTime startDateTime, DateTime endDateTime);
        int SaveSchedule(WsSchedule schedule);
        int DeleteSchedule(WsSchedule schedule);
        int AddShopSchedule(WsSchedule schedule);
    }

    public class MangoDbContext : IMangoDbContext
    {
        private readonly IMongoDatabase _database;

        public MangoDbContext()
        {
            IMongoClient client = new MongoClient("mongodb://REPLACE_ME:REPLACE_ME@REPLACE_ME:27017/REPLACE_ME");
            _database = client.GetDatabase("REPLACE_ME");
        }


        public int DeleteShopService(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("service");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            collection.DeleteOne(filter);

            return 1;
        }

        public int DeleteShopUser(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUser");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            collection.DeleteOne(filter);

            return 1;
        }

        public int DeleteVoucherType(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("voucherType");
            
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            collection.DeleteOne(filter);
            
            return 1;
        }

        public ShopService GetShopServiceById(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("service");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return new ShopService
            {
                ShopId = shopId,
                //Created = result[0]["created"].ToUniversalTime(),
                //Updated = result[0]["updated"].ToUniversalTime(),
                Id = result[0]["id"].ToInt32(),
                Name = result[0]["name"].AsString,
                Description = result[0]["description"].AsString,
                Category = result[0]["category"].AsString,
                Length = result[0]["length"].ToInt32(),
                MaxPerDay = result[0]["maxPerDay"].ToInt32(),
                PriceFrom = result[0]["priceFrom"].ToInt32(),
                PriceTo = result[0]["priceTo"].ToInt32()
            };
        }

        public ShopUser GetShopUserById(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUser");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return new ShopUser
            {
                ShopId = shopId,
                Created = DateTime.Parse(result[0]["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(result[0]["updated"].AsBsonDateTime.ToString()),
                Id = result[0]["id"].AsInt32,
                Email = result[0]["email"].AsString,
                IsActive = result[0]["isActive"].AsBoolean,
                Password = result[0]["password"].AsString,
                DisplayName = result[0]["displayName"].AsString,
                UserName = result[0]["userName"].AsString,
                UserGroupId = result[0]["userGroupId"].AsInt32,
                Pin = result[0]["pin"].AsString
            };
        }

        public VoucherType GetVoucherTypeById(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("voucherType");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return new VoucherType
            {
                ShopId = shopId,
                Created = DateTime.Parse(result[0]["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(result[0]["updated"].AsBsonDateTime.ToString()),
                Id = result[0]["id"].ToInt32(),
                ServiceId = result[0]["serviceId"].ToInt32(),
                Name = result[0]["name"].AsString,
                Desc = result[0]["desc"].AsString
            };
        }

        public List<VoucherType> GetAllVoucherTypes(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("voucherType");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new VoucherType
            {
                Id = s["id"].AsInt32,
                ShopId = shopId,
                Created = DateTime.Parse(s["created"].AsBsonDateTime.ToString()),
                Name = s["name"].AsString,
                Desc = s["desc"].AsString,
                ServiceId = s["serviceId"].AsInt32
            }).ToList();
        }

        //Update
        public int SaveCustomer(ShopCustomer customer, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("email", customer.Email) & builder.Eq("shopId", shopId) & builder.Eq("id", customer.Id);
            var update = Builders<BsonDocument>.Update
                .Set("fullName", customer.ContactName)
                .Set("mobile", customer.PhoneNumber)
                .Set("email", customer.Email)
                //.Set("password", shopCustomer.Password)
                .Set("address", customer.Address)
                .Set("isactive", customer.IsActive)
                .Set("firstName", customer.FirstName)
                .Set("lastName", customer.LastName)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);
            return 1;
        }

        public int SaveOrUpdateShopService(ShopService shopService, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("service");
            
            if (shopService.Id == 0)
            {
                var service = new BsonDocument
                {
                    { "shopId", shopId},
                    { "created", DateTime.Now},
                    { "updated", DateTime.Now},
                    { "name", shopService.Name},
                    { "description", shopService.Description},
                    { "category", shopService.Category},
                    { "length", shopService.Length},
                    { "maxPerDay", shopService.MaxPerDay},
                    { "priceFrom", shopService.PriceFrom},
                    { "priceTo", shopService.PriceTo},
                    { "id", DateTime.Now.Millisecond }
                };

                collection.InsertOne(service);
                return 1;
            }
            
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", shopService.Id) & builder.Eq("shopId", shopId);
            
            var update = Builders<BsonDocument>.Update
                .Set("name", shopService.Name)
                .Set("description", shopService.Description)
                .Set("category", shopService.Category)
                .Set("length", shopService.Length)
                .Set("maxPerDay", shopService.MaxPerDay)
                .Set("priceFrom", shopService.PriceFrom)
                .Set("priceTo", shopService.PriceTo);

            var result = collection.UpdateOne(filter, update);
            return 1;
        }

        public int SaveOrUpdateShopUser(ShopUser shopUser, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUser");
            
            if (shopUser.Id == 0)
            {
                var newShopUser = new BsonDocument
                {
                    { "shopId", shopId},
                    { "created", DateTime.Now},
                    { "updated", DateTime.Now},
                    { "email", shopUser.Email},
                    { "isActive", shopUser.IsActive},
                    { "password", shopUser.Password},
                    { "displayName", shopUser.DisplayName},
                    { "userName", shopUser.UserName},
                    { "userGroupId", shopUser.UserGroupId},
                    { "pin", shopUser.Pin},
                    { "id", DateTime.Now.Millisecond }
                };

                collection.InsertOne(newShopUser);
                return 1;
            }

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", shopUser.Id) & builder.Eq("shopId", shopId);
            var update = Builders<BsonDocument>.Update
                .Set("email", shopUser.Email)
                .Set("isActive", shopUser.IsActive)
                .Set("password", shopUser.Password)
                .Set("displayName", shopUser.DisplayName)
                .Set("userName", shopUser.UserName)
                .Set("userGroupId", shopUser.UserGroupId)
                .Set("pin", shopUser.Pin)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);
            return 1;
        }

        //Save or Update voucher type
        public int SaveOrUpdateVoucherType(VoucherType voucher, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("voucherType");

            if (voucher.Id == 0)
            {
                var newVoucherType = new BsonDocument
                {
                    { "shopId", shopId},
                    { "created", DateTime.Now},
                    { "updated", DateTime.Now},
                    { "name", voucher.Name},
                    { "desc", voucher.Desc},
                    { "serviceId", voucher.ServiceId},
                    { "id", DateTime.Now.Millisecond }
                };

                collection.InsertOne(newVoucherType);
                return 1;
            }
           
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", voucher.Id) & builder.Eq("shopId", shopId);
            var update = Builders<BsonDocument>.Update
                .Set("name", voucher.Name)
                .Set("desc", voucher.Desc)
                .Set("serviceId", voucher.ServiceId)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);
            return 1;
        }

        public string SendUserVoucher(int customerId, int shopId, int voucherId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", customerId) & builder.Eq("shopId", shopId);

            var currentVouchers = collection.Find(filter).ToList()[0]["vouchers"].ToString();
            var newVouchers = string.IsNullOrEmpty(currentVouchers) ? voucherId.ToString() : currentVouchers + "," + voucherId;

            var update = Builders<BsonDocument>.Update
                .Set("vouchers", newVouchers)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);

            var voucherRef = Guid.NewGuid();

            var voucher = new BsonDocument
            {
                { "shopId", shopId},
                { "created", DateTime.Now},
                { "updated", DateTime.Now },
                { "id", DateTime.Now.Millisecond},
                { "typeId", 1},
                { "ref", voucherRef}
            };

            var voucherCollection = _database.GetCollection<BsonDocument>("voucher");
            voucherCollection.InsertOne(voucher);
            
            return voucherRef.ToString();
        }

        public List<ContactForm> GetUserFeedback(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("contactform");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new ContactForm
            {
                ShopId = shopId,
                Created = DateTime.Parse(s["created"].AsBsonDateTime.ToString()),
                Email = s["email"].AsString,
                Message = s["message"].AsString,
                Id = s["id"].AsInt32,
                Name = s["name"].AsString,
                Subject = s["subject"].AsString,
                Mobile = s["mobile"].AsString
            }).ToList();
        }

        public string UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", bookingId) & builder.Eq("shopId", shopId);

            var newBookingStatus = ((BookingStatusEnum) bookingStatus).ToString();

            var update = Builders<BsonDocument>.Update
                .Set("status", newBookingStatus)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);

            return newBookingStatus;
        }

        public string UpdateUserCredit(int customerId, int shopId, int credits)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", customerId) & builder.Eq("shopId", shopId);

            var currentCredits = collection.Find(filter).ToList()[0]["credit"].ToInt32();
            var newCredits = currentCredits + credits;

            var update = Builders<BsonDocument>.Update
                .Set("credit", newCredits)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);

            return newCredits.ToString();
        }

        #region ShopUser
        public ShopUser LoginShopAgent(string email, string password, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUser");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("email", email) & builder.Eq("shopId", shopId) & builder.Eq("password", password) & builder.Eq("isActive", true);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return new ShopUser
            {
                Email = result[0]["email"].AsString,
                ShopId = shopId,
                Created = DateTime.Parse(result[0]["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(result[0]["updated"].AsBsonDateTime.ToString()),
                Id = result[0]["id"].ToInt32(),
                Password = password,
                IsActive = result[0]["isActive"].AsBoolean,
                UserGroupId = result[0]["userGroupId"].ToInt32(),
                UserName = result[0]["userName"].AsString,
                DisplayName = result[0]["displayName"].AsString
            };
        }

        #endregion

        #region Customer
        public void CreateCustomer(ShopCustomer shopCustomer, int shopId)
        {
            var customer = new BsonDocument
            {
                { "shopId", shopId},
                { "created", DateTime.Now},
                { "updated", DateTime.Now },
                { "fullName", shopCustomer.ContactName },
                { "mobile", shopCustomer.PhoneNumber },
                { "email", shopCustomer.Email},
                { "password", shopCustomer.Password},
                { "address", shopCustomer.Address},
                { "credit", shopCustomer.StoreCredit},
                { "isactive", shopCustomer.IsActive },
                { "firstName", shopCustomer.FirstName },
                { "lastName", shopCustomer.LastName },
                { "id", DateTime.Now.Millisecond }
            };

            var collection = _database.GetCollection<BsonDocument>("customer");
            collection.InsertOne(customer);
        }
        
        public ShopCustomer GetCustomerById(int id, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", id) & builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return new ShopCustomer
            {
                Email = result[0]["email"].AsString,
                ShopId = shopId,
                PhoneNumber = result[0]["mobile"].AsString,
                IsActive = true,
                Password = result[0]["password"].AsString,
                Created = DateTime.Parse(result[0]["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(result[0]["updated"].AsBsonDateTime.ToString()),
                Id = result[0]["id"].ToInt32(),
                FirstName = result[0]["firstName"].AsString,
                LastName = result[0]["lastName"].AsString
            };
        }

        public ShopCustomer GetCustomerByEmailAddress(string email, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("email", email) & builder.Eq("shopId", shopId) & builder.Eq("isactive", true);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any() ) return null;

            return new ShopCustomer
            {
                Address = result[0]["address"].AsString,
                Email = result[0]["email"].AsString,
                ShopId = shopId,
                PhoneNumber = result[0]["mobile"].AsString,
                IsActive = true,
                Password = result[0]["password"].AsString,
                Created = DateTime.Parse(result[0]["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(result[0]["updated"].AsBsonDateTime.ToString()),
                FirstName = result[0]["firstName"].AsString,
                LastName = result[0]["lastName"].AsString
            };
        }

        public ShopCustomer GetCustomerByEmailPassword(string email, string password, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("email", email) & builder.Eq("shopId", shopId) & builder.Eq("password", password);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return new ShopCustomer
            {
                Address = result[0]["address"].AsString,
                Email = result[0]["email"].AsString,
                ShopId = shopId,
                PhoneNumber = result[0]["mobile"].AsString,
                IsActive = true,
                Created = DateTime.Parse(result[0]["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(result[0]["updated"].AsBsonDateTime.ToString()),
                Id = result[0]["id"].ToInt32(),
                FirstName = result[0]["firstName"].AsString,
                LastName = result[0]["lastName"].AsString
            };
        }

        public void UpdateCustomer(ShopCustomer shopCustomer, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("email", shopCustomer.Email) & builder.Eq("shopId", shopId) & builder.Eq("id", shopCustomer.Id);
            var update = Builders<BsonDocument>.Update
                .Set("fullName", shopCustomer.ContactName)
                .Set("mobile", shopCustomer.PhoneNumber)
                .Set("email", shopCustomer.Email)
                //.Set("password", shopCustomer.Password)
                .Set("address", shopCustomer.Address)
                .Set("isactive", shopCustomer.IsActive)
                .Set("firstName", shopCustomer.FirstName)
                .Set("lastName", shopCustomer.LastName)
                .CurrentDate("updated");
            
            var result = collection.UpdateOne(filter, update);
        }
        #endregion

        #region Booking
        public bool CancelBooking(int customerId, int shopId, int bookingId)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("customerId", customerId) & builder.Eq("shopId", shopId) & builder.Eq("id", bookingId);
            var update = Builders<BsonDocument>.Update
                .Set("status", BookingStatusEnum.Cancelled.ToString())
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);

            return true;
        }
        
        public string BookService(Booking request)
        {
            var id = Guid.NewGuid().ToString();

            var booking = new BsonDocument
            {
                { "shopId", request.ShopId},
                { "created", DateTime.Now},
                { "updated", DateTime.Now },
                { "date", request.DateTimeSlot },
                { "customerId", request.CustomerId },
                { "serviceId", request.ServiceId },
                { "status", BookingStatusEnum.New.ToString() },
                { "shopUserId", request.ShopUserId},
                { "id", id}
            };

            var collection = _database.GetCollection<BsonDocument>("booking");
            collection.InsertOne(booking);

            return id;
        }

        public bool AddSchedule(Booking request)
        {
            var service = GetShopServiceById(request.ServiceId, request.ShopId);
            var customer = GetCustomerById(request.CustomerId, request.ShopId);

            var id = Guid.NewGuid().ToString();

            var booking = new BsonDocument
            {
                { "shopId", request.ShopId},
                { "created", DateTime.Now},
                { "updated", DateTime.Now },
                { "startDate", request.DateTimeSlot },
                { "subject", service.Name },
                { "length", service.Length },
                { "description", service.Description + " with " + customer.ContactName },
                { "customerId", request.CustomerId },
                { "bookingId", request.BookingId },
                { "serviceId", request.ServiceId },
                { "shopUserId", request.ShopUserId },
                { "status", "Booked" },
                { "id", id}
            };

            var collection = _database.GetCollection<BsonDocument>("shopUserScheduler");
            collection.InsertOne(booking);

            return true;
        }

        public List<Booking> GetAllActiveBookingsForCustomer(int customerId, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("customerId", customerId) & builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new Booking
            {
                ShopId = shopId,
                DateTimeSlot = DateTime.Parse(s["date"].AsBsonDateTime.ToString()),
                CustomerId = customerId,
                ServiceId = s["serviceId"].AsInt32,
                Status = s["status"].AsString,
                BookingId = s["id"].AsString,
                ShopUserId = s["shopUserId"].AsInt32,
                
            }).ToList();
        }

        public int DeleteCustomerBookings(int customerId, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("customerId", customerId) & builder.Eq("shopId", shopId);

            collection.DeleteMany(filter);

            return 1;
        }

        public List<ShopUserScheduler> GetBookedSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUserScheduler");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopUserId", shopUserId) 
                         & builder.Eq("shopId", shopId) 
                         & builder.Gte("startDate", startDateTime.Date)
                         & builder.Lt("startDate", startDateTime.Date.AddDays(1));

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return result.Select(s=> new ShopUserScheduler
            {
                ShopId = shopId,
                ShopUserId = shopUserId,
                ScheduleId = s["id"].AsString,
                StartDateTime = DateTime.Parse(s["startDate"].AsBsonDateTime.ToString()),
                Length = s["length"].AsInt32 ,
                ServiceId = s["serviceId"].AsInt32,
                BookingId = s["bookingId"].AsString,
                CustomerId = s["customerId"].AsInt32,
                Description = s["description"].AsString,
                Subject = s["subject"].AsString
            }).ToList();
        }

        public List<ShopUserScheduler> GetAllBookedSlotsForScheduler(int shopId, DateTime startDateTime, DateTime endDateTime)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUserScheduler");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId)
                         & builder.Gte("startDate", startDateTime.Date)
                         & builder.Lt("startDate", endDateTime.Date);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return result.Select(s => new ShopUserScheduler
            {
                ShopId = shopId,
                ShopUserId = s["shopUserId"].AsInt32,
                ScheduleId = s["id"].AsString,
                StartDateTime = DateTime.Parse(s["startDate"].AsBsonDateTime.ToString()),
                Length = s["length"].AsInt32,
                ServiceId = s["serviceId"].AsInt32,
                BookingId = s["bookingId"].AsString,
                CustomerId = s["customerId"].AsInt32,
                Description = s["description"].AsString,
                Subject = s["subject"].AsString,
                Status = s["status"].AsString
            }).ToList();
        }

        public int SaveSchedule(WsSchedule schedule)
        {
            var shopUserId = GetAllShopUser(schedule.ShopId.Value).FirstOrDefault(x => x.DisplayName == schedule.ShopUserName).Id;

            var collection = _database.GetCollection<BsonDocument>("shopUserScheduler");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", schedule.ShopId) 
                         & builder.Eq("id", schedule.ScheduleId);

            var update = Builders<BsonDocument>.Update
                .Set("startDate", schedule.StartDateTime)
                .Set("length", (int)schedule.EndDateTime.Subtract(schedule.StartDateTime).TotalMinutes)
                .Set("description", schedule.Description)
                .Set("subject", schedule.Subject)
                .Set("status", schedule.Status)
                .Set("shopUserId", shopUserId)
                .CurrentDate("updated");

            var result = collection.UpdateOne(filter, update);

            return 1;
        }

        public int DeleteSchedule(WsSchedule schedule)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUserScheduler");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", schedule.ShopId)
                         & builder.Eq("id", schedule.ScheduleId);

            collection.DeleteOne(filter);

            return 1;
        }

        public int AddShopSchedule(WsSchedule schedule)
        {
            var shopUserId = GetAllShopUser(schedule.ShopId.Value).FirstOrDefault(x => x.DisplayName == schedule.ShopUserName).Id;

            var scheduleDoc = new BsonDocument
            {
                {"shopUserId", shopUserId},
                {"created", DateTime.Now},
                {"updated", DateTime.Now},
                {"status", schedule.Status},
                {"startDate", schedule.StartDateTime},
                {"length", (int) schedule.EndDateTime.Subtract(schedule.StartDateTime).TotalMinutes},
                {"description", schedule.Description},
                {"subject", schedule.Subject},
                {"customerId", 0},
                {"serviceId", 0},
                {"shopId", schedule.ShopId},
                {"id", Guid.NewGuid().ToString()},
                {"bookingId", Guid.NewGuid().ToString()}
            };

            var collection = _database.GetCollection<BsonDocument>("shopUserScheduler");

            collection.InsertOne(scheduleDoc);

            return 1;
        }

        public ShopSettings GetShopSettings(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopSettings");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();

            if (result == null || !result.Any()) return null;

            return new ShopSettings
            {
                ShopId = shopId,
                Name = result[0]["shopName"].AsString,
                LogoUrl = result[0]["logoUrl"].AsString,
                OpenTime = DateTime.Parse(result[0]["openTime"].AsBsonDateTime.ToString()),
                CloseTime = DateTime.Parse(result[0]["closeTime"].AsBsonDateTime.ToString())
            };
        }
        
        public List<Booking> GetAllBookingsByCustomerId(int customerId, int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("customerId", customerId) & builder.Eq("shopId", shopId) & builder.Gte("created", DateTime.Now.AddDays(-60));

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new Booking
            {
                ShopId = shopId,
                DateTimeSlot = DateTime.Parse(s["date"].AsBsonDateTime.ToString()),
                CustomerId = customerId,
                ServiceId = s["serviceId"].AsInt32,
                Status = s["status"].AsString,
                BookingId = s["id"].AsString
            }).ToList();
        }

        public List<Booking> GetAllBookingsForShopUserOnDate(int shopId, int shopUserId, DateTime scheduledDate)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopUserId", shopUserId) 
                         & builder.Eq("shopId", shopId) 
                         & builder.Gte("date", scheduledDate.Date)
                         & builder.Lt("date", scheduledDate.Date.AddDays(1));

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new Booking
            {
                ShopId = shopId,
                DateTimeSlot = DateTime.Parse(s["date"].AsBsonDateTime.ToString()),
                CustomerId = s["customerId"].AsInt32,
                ServiceId = s["serviceId"].AsInt32,
                Status = s["status"].AsString,
                BookingId = s["id"].AsString,
                ShopUserId = s["shopUserId"].AsInt32
            }).ToList();
        }

        public List<Booking> GetAllBookingsByShopId(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("booking");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new Booking
            {
                ShopId = shopId,
                DateTimeSlot = DateTime.Parse(s["date"].AsBsonDateTime.ToString()),
                CustomerId = s["customerId"].AsInt32,
                ServiceId = s["serviceId"].AsInt32,
                Status = s["status"].AsString,
                BookingId = s["id"].AsString
            }).ToList();
        }

        public List<ShopCustomer> GetAllCustomersByShopId(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("customer");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new ShopCustomer
            {           
                ShopId = shopId,
                Created = DateTime.Parse(s["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(s["updated"].AsBsonDateTime.ToString()),
                FirstName = s["firstName"].AsString,
                LastName = s["lastName"].AsString,
                PhoneNumber = s["mobile"].AsString,
                Id = s["id"].AsInt32,
                StoreCredit = s["credit"].AsInt32,
                Email = s["email"].AsString,
                Password = s["password"].AsString
            }).ToList();
        }
        
        #endregion

        public List<ShopUser> GetAllShopUser(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopUser");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new ShopUser
            {
                ShopId = shopId,
                Created = DateTime.Parse(s["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(s["updated"].AsBsonDateTime.ToString()),
                Id = s["id"].AsInt32,
                Email = s["email"].AsString,
                IsActive = s["isActive"].AsBoolean,
                Password = s["password"].AsString,
                DisplayName = s["displayName"].AsString,
                UserName = s["userName"].AsString,
                UserGroupId = s["userGroupId"].AsInt32,
                Pin = s["pin"].AsString
            }).ToList();
        }

        public List<ShopServiceUser> GetShopServiceUser(int shopId)
        {
            var collection = _database.GetCollection<BsonDocument>("shopServiceUser");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result.Select(s => new ShopServiceUser
            {
                ServiceId = s["serviceId"].AsInt32,
                ShopId = shopId,
                UserId = s["userId"].AsInt32,
                Id = s["id"].AsInt32
            }).ToList();
        }

        #region ShopService
        public List<ShopService> GetShopService(int shopId)
        {
            //1. Shop Service
            var collection = _database.GetCollection<BsonDocument>("service");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("shopId", shopId);
            
            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            var shopServiceList = result.Select(s => new ShopService
                                  {
                                      Name = s["name"].AsString,
                                      ShopId = shopId,
                                      Category = s["category"].AsString,
                                      Description = s["description"].AsString,
                                      Id = s["id"].AsInt32,
                                      Length = s["length"].AsInt32,
                                      MaxPerDay = s["maxPerDay"].AsInt32,
                                      PriceFrom = s["priceFrom"].AsInt32,
                                      PriceTo = s["priceTo"].AsInt32,
                                      ServiceUsers = new List<ShopUser>()
                                  }).ToList();

            //2. Shop Service User
            var collection2 = _database.GetCollection<BsonDocument>("shopServiceUser");
            var builder2 = Builders<BsonDocument>.Filter;
            var filter2 = builder2.Eq("shopId", shopId);

            var result2 = collection2.Find(filter2).ToList();
            if (result2 == null || !result2.Any()) return null;
            
            var shopServiceUserList = result2.Select(s => new ShopServiceUser
            {
                ServiceId = s["serviceId"].AsInt32,
                ShopId = shopId,
                UserId = s["userId"].AsInt32,
                Id = s["id"].AsInt32
            }).ToList();

            //3. Shop User
            var collection3 = _database.GetCollection<BsonDocument>("shopUser");
            var builder3 = Builders<BsonDocument>.Filter;
            var filter3 = builder3.Eq("shopId", shopId);

            var result3 = collection3.Find(filter3).ToList();
            if (result3 == null || !result3.Any()) return null;

            var shopUserList = result3.Select(s => new ShopUser
            {
                ShopId = shopId,
                Created = DateTime.Parse(s["created"].AsBsonDateTime.ToString()),
                Updated = DateTime.Parse(s["updated"].AsBsonDateTime.ToString()),
                Id = s["id"].AsInt32,
                Email = s["email"].AsString,
                IsActive = s["isActive"].AsBoolean,
                Password = s["password"].AsString,
                DisplayName = s["displayName"].AsString,
                UserName = s["userName"].AsString,
                UserGroupId = s["userGroupId"].AsInt32,
                Pin = s["pin"].AsString
            }).ToList();
            
            foreach (var user in shopUserList)
            {
                foreach (var service in shopServiceList)
                {
                    foreach (var serviceUser in shopServiceUserList)
                    {
                        if (serviceUser.ServiceId == service.Id && serviceUser.UserId == user.Id)
                        {
                            service.ServiceUsers.Insert(0, user);
                        }
                    }
                }
            }

            foreach (var service in shopServiceList)
            {
                service.ServiceUsers.Add(new ShopUser { Id = -1, DisplayName = "Any", IsActive = true, UserName = "Any" });
            }

            return shopServiceList;
        }

        public string GetServiceName(int serviceId)
        {
            var collection = _database.GetCollection<BsonDocument>("service");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("id", serviceId);

            var result = collection.Find(filter).ToList();
            if (result == null || !result.Any()) return null;

            return result[0]["name"].AsString;
        }

        #endregion

        #region Contact
        public bool SaveContactForm(ContactForm contactForm, int shopId)
        {
            var contactInfo = new BsonDocument
            {
                { "shopId", shopId},
                { "created", DateTime.Now},
                { "name", contactForm.Name },
                { "email", contactForm.Email },
                { "message", contactForm.Message},
                { "subject", "Contact Request"},
                { "mobile", contactForm.Mobile},
                { "id", DateTime.Now.Millisecond}
            };

            var collection = _database.GetCollection<BsonDocument>("contactform");
            collection.InsertOne(contactInfo);

            return true;
        }

        #endregion
    }
}
