using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using App.Service.Contract.Entities.Authentication;
using App.Web.Application.Utility;
using Core.Common.Utility;
using Newtonsoft.Json;

namespace App.Web.Application.Filter
{
    public class ShortTokenValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            try
            {
                var token = actionContext.Request.Headers.GetValues("IndaSoftAdminToken").First();
                var decryptedToken = new TokenEncryptor().Decrypt(token);

                var userInfo = JsonConvert.DeserializeObject<WsShopCustomer>(decryptedToken);

                if (string.IsNullOrEmpty(userInfo?.Email) || !userInfo.TokenExpire.HasValue)
                    throw new Exception("Unauthorized User");

                if (userInfo.TokenExpire.Value < DateTime.Now)
                    throw new Exception("Unauthorized User");

                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("TokenValidationAttribute:[Exception]Unauthorized User. {0}-{1}", ex.Message, ex.StackTrace));
                actionContext.Response =
                  new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                  {
                      Content = new StringContent("Unauthorized User")
                  };
                return;
            }
        }
    }
}