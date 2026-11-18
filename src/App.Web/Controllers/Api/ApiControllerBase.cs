using System;
using System.Linq;
using System.Web.Http;
using App.Service.Exception;
using App.Web.Application.Extension;
using Core.Common.Exception;
using Core.Common.Utility;
using log4net;

namespace App.Web.Controllers.Api
{
    public interface IApiControllerBase
    {
        IHttpActionResult Execute<T>(string operation, int shopId,Func<T> action);
    }

    public class ApiControllerBase : ApiController, IApiControllerBase
    {
        protected readonly ILog Logger;

        public ApiControllerBase()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }

        public IHttpActionResult Execute<T>(string operation, int shopId, Func<T> action)
        {
            var sessionId = Guid.NewGuid().ToString();
            try
            {
                Logger.DebugFormat("[Operation:{0}][SessionId:{1}] Perform Action", operation, sessionId);
                //TODO: Check Token
                //CheckLongToken(shopId);

                return Ok(action());

            }
            catch (ServiceException ex)
            {
                return HandleException(operation, sessionId, ex);
            }
            catch (Exception ex)
            {
                return HandleException(operation, sessionId, ex);
            }


            /*The following code will get throw back to client 
             200 OK -  Good Request
             403 Forbidden  -  Not Long Key
             401 Unauthorized - No short key
             404 Not Found  - Server Error
            */
        }

        public IHttpActionResult ExecuteInit<T>(string operation, Func<T> action)
        {
            var sessionId = Guid.NewGuid().ToString();
            try
            {
                Logger.DebugFormat("[Operation:{0}][SessionId:{1}] Perform Action", operation, sessionId);
                return Ok(action());

            }
            catch (ServiceException ex)
            {
                return HandleException(operation, sessionId, ex);
            }
            catch (Exception ex)
            {
                return HandleException(operation, sessionId, ex);
            }
            
            /*The following code will get throw back to client 
             200 OK -  Good Request
             403 Forbidden  -  Not Long Key
             401 Unauthorized - No short key
             404 Not Found  - Server Error
            */
        }
        
        private IHttpActionResult HandleException(string operation, string sessionId, Exception ex)
        {
            Logger.ErrorFormat("[Operation:{0}][SessionId:{1}] Error:{2}--{3}", operation, sessionId, ex.Message, ex.StackTrace);
            var unHandleException = new CoreException("UnHandleWcf", CoreExceptionCode.Ex001.ToString(), ex);

            return ApiControllerExtension.NotFound(this, ex.Message);
        }

        private IHttpActionResult HandleException(string operation, string sessionId, CoreException ex)
        {
            Logger.ErrorFormat("[Operation:{0}][SessionId:{1}] Error:{2}--{3}", operation, sessionId, ex.Message, ex.StackTrace);
            return ApiControllerExtension.NotFound(this, ex.Message);
        }

        protected void CheckLongToken(int shopId)
        {
            var headers = Request.Headers;
            var token = headers.GetValues("BeautyFairyAuthToken").FirstOrDefault();
            var decryptedToken = new TokenEncryptor().Decrypt(token);
            var decryptedTokensplit = decryptedToken.Split(',');

            if (decryptedTokensplit[1].Trim() != shopId.ToString())
                throw new Exception("Unauthorized User");
        }
    }
}