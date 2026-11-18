using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using App.Web.Application.Utility;
using Core.Common.Utility;

namespace App.Web.Application.Filter
{
    public class ApiValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            string token;

            try
            {
                token = actionContext.Request.Headers.GetValues("IndaSoftAuthUserToken").First();
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("ApiValidationAttribute:[Exception]Missing IndaSoftAuthUserToken Token. {0}-{1}", ex.Message, ex.StackTrace));
                actionContext.Response =
                  new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                  {
                      Content = new StringContent("Missing IndaSoftAuthUserToken Token")
                  };
                return;
            }

            //try
            //{
            //    var decryptedToken = TokenEncryptor.Decrypt(token);
            //    var validator = new ShopUserValidator();
            //    if(!validator.RefreshValidationToken(decryptedToken))
            //        throw new Exception("Unauthorsed User");


            //    base.OnActionExecuting(actionContext);
            //}
            //catch (Exception ex)
            //{
            //    Trace.TraceError(string.Format("ApiValidationAttribute:[Exception]Unauthorized User. {0}-{1}", ex.Message, ex.StackTrace));
            //    actionContext.Response =
            //      new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
            //      {
            //          Content = new StringContent("Unauthorized User")
            //      };
            //    return;
            //}
        }
    }
}