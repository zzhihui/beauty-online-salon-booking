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
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            try
            {
                //var token = actionContext.Request.Headers.GetValues("IndaSoftAuthToken").First();
                //var decryptedToken = new TokenEncryptor().Decrypt(token);
                //var decryptedTokensplit = decryptedToken.Split(',');

                //if (!Constants.ClientKeys.Contains(decryptedTokensplit[0]))
                //    throw new Exception("Forbidden Request");


                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("TokenValidationAttribute:[Exception]Forbidden Request. {0}-{1}", ex.Message, ex.StackTrace));
                actionContext.Response =
                  new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                  {
                      Content = new StringContent("Forbidden Request")
                  };
                return;
            }
        }
    }
}