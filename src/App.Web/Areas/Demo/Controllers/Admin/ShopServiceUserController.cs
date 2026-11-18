using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;
using App.Web.ViewModels;

namespace App.Web.Areas.Demo.Controllers.Admin
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
            var serviceUserList = _shopUserServiceClient.GetShopServiceUser(ShopId).Content;
            var serviceList = _shopUserServiceClient.GetAllShopService(ShopId).Content;
            var userList = _shopUserServiceClient.GetAllShopUser(ShopId).Content;

            var serviceUserViewModel = new ServiceUserViewModel {ServiceUsers = new List<ServiceUser>()};

            foreach (var user in userList)
            {
                //var vmServiceList = serviceList.Select(service => new ViewModels.Service
                //{
                //    ServiceName = service.Name, IsAssignedToUser = serviceUserList.Any(x => x.UserId == user.Id && service.Id == x.ServiceId)
                //}).ToList();

                var vmServiceList = new List<ViewModels.Service>();

                foreach (var service in serviceList)
                {
                    var vmService = new ViewModels.Service
                    {
                        ServiceName = service.Name,
                        IsAssignedToUser = false
                    };

                    foreach (var serviceUser in serviceUserList)
                    {
                        if (serviceUser.ServiceId == service.Id && serviceUser.UserId == user.Id)
                        {
                            vmService.IsAssignedToUser = true;
                            break;
                        }
                    }
                    vmServiceList.Add(vmService);
                    
                }

                serviceUserViewModel.ServiceUsers.Add
                (
                    new ServiceUser
                    {
                        UserId = user.Id,
                        UserDisplayName = user.DisplayName,
                        ServiceList = vmServiceList
                    }
                );
            }
            
            return View(serviceUserViewModel);
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