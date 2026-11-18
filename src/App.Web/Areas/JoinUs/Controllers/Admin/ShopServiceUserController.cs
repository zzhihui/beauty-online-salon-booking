using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;
using App.Web.ViewModels;

namespace App.Web.Areas.JoinUs.Controllers.Admin
{
    public class ShopServiceUserController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public ShopServiceUserController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [AreaAuth]
        public ActionResult List()
        {
            //var serviceUserList = _shopUserServiceClient.GetShopServiceUser(ShopId).Content;
            //var serviceList = _shopUserServiceClient.GetAllShopServices(ShopId).Content;
            //var userList = _shopUserServiceClient.GetAllShopUsers(ShopId).Content;

            var serviceUserViewModel = new ServiceUserViewModel {ServiceUsers = new List<ServiceUser>()};

            //foreach (var user in userList)
            //{
            //    //var vmServiceList = serviceList.Select(service => new ViewModels.Service
            //    //{
            //    //    ServiceName = service.Name, IsAssignedToUser = serviceUserList.Any(x => x.UserId == user.Id && service.Id == x.ServiceId)
            //    //}).ToList();

            //    var vmServiceList = new List<ViewModels.Service>();

            //    foreach (var service in serviceList)
            //    {
            //        var vmService = new ViewModels.Service
            //        {
            //            ServiceName = service.Name,
            //            ServiceId = service.Id,
            //            IsAssignedToUser = false
            //        };

            //        if (serviceUserList != null && serviceUserList.Any())
            //        {
            //            foreach (var serviceUser in serviceUserList)
            //            {
            //                if (serviceUser.ServiceId == service.Id && serviceUser.UserId == user.Id)
            //                {
            //                    vmService.IsAssignedToUser = true;
            //                    break;
            //                }
            //            }
            //        }
                    
            //        vmServiceList.Add(vmService);
                    
            //    }

            //    serviceUserViewModel.ServiceUsers.Add
            //    (
            //        new ServiceUser
            //        {
            //            UserId = user.Id,
            //            UserDisplayName = user.DisplayName,
            //            ServiceList = vmServiceList
            //        }
            //    );
            //}
            
            return View(serviceUserViewModel);
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult List(ServiceUserViewModel model)
        {
            //var newServiceUserList = new List<WsShopServiceUser>();

            //foreach (var serviceUser in model.ServiceUsers)
            //{
            //    foreach (var service in serviceUser.ServiceList)
            //    {
            //        if (service.IsAssignedToUser)
            //        {
            //            var newServiceUser = new WsShopServiceUser {
            //                ShopId = ShopId,
            //                Updated = DateTime.Now,
            //                UserId = serviceUser.UserId,
            //                ServiceId = service.ServiceId
            //            };
            //            newServiceUserList.Add(newServiceUser);
            //        }
            //    }
            //}

            //_shopUserServiceClient.SaveShopServiceUser(newServiceUserList, ShopId);

            //var serviceUserList = _shopUserServiceClient.GetShopServiceUser(ShopId).Content;
            //var serviceList = _shopUserServiceClient.GetAllShopServices(ShopId).Content;
            //var userList = _shopUserServiceClient.GetAllShopUsers(ShopId).Content;

            //var serviceUserViewModel2 = new ServiceUserViewModel { ServiceUsers = new List<ServiceUser>() };

            //foreach (var user in userList)
            //{
            //    //var vmServiceList = serviceList.Select(service => new ViewModels.Service
            //    //{
            //    //    ServiceName = service.Name, IsAssignedToUser = serviceUserList.Any(x => x.UserId == user.Id && service.Id == x.ServiceId)
            //    //}).ToList();

            //    var vmServiceList = new List<ViewModels.Service>();

            //    foreach (var service in serviceList)
            //    {
            //        var vmService = new ViewModels.Service
            //        {
            //            ServiceName = service.Name,
            //            //ServiceId = service.Id,
            //            IsAssignedToUser = false
            //        };

            //        if (serviceUserList != null && serviceUserList.Any())
            //        {
            //            foreach (var serviceUser in serviceUserList)
            //            {
            //                if (serviceUser.ServiceId == service.Id && serviceUser.UserId == user.Id)
            //                {
            //                    vmService.IsAssignedToUser = true;
            //                    break;
            //                }
            //            }
            //        }

            //        vmServiceList.Add(vmService);
            //    }

            //    serviceUserViewModel2.ServiceUsers.Add
            //    (
            //        new ServiceUser
            //        {
            //            UserId = user.Id,
            //            UserDisplayName = user.DisplayName,
            //            ServiceList = vmServiceList
            //        }
            //    );
            //}

            return View(model);
        }
        
        [AreaAuth]
        public ActionResult Edit(int id)
        {
            ViewBag.result = string.Empty;

            var voucher = _shopUserServiceClient.GetVoucherTypeById(id, ShopId);

            return View(voucher.Content);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody]WsVoucherType voucher)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveVoucherType(voucher, ShopId);

            return View(voucher);
        }
    }
}