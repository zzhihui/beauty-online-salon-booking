using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Service.Contract.Entities.Settings;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class SettingApiController : ApiControllerBase
    {

        private readonly ISettingServiceHandler _settingServiceHandler;

        public SettingApiController(ISettingServiceHandler settingServiceHandler)
        {
            _settingServiceHandler = settingServiceHandler;
        }


        [HttpGet]
        public IHttpActionResult GetSystemSettings(int shopId)
        {
            return Execute("GetSystemSettings", shopId, () => _settingServiceHandler.GetSystemSettings(shopId));
        }


        [HttpPut]
        public IHttpActionResult UpdateSystemSettings(WsSetting setting,int shopId)
        {
            return Execute("UpdateSystemSettings", shopId, () => _settingServiceHandler.UpdateSystemSettings(setting,shopId));

        }
    }
}
