using System.Web.Http;
using App.Service.Services;

namespace App.Web.Controllers.Api
{
    public class AuthenticationApiController: ApiControllerBase
    {
        private readonly IAuthenticationServiceHandler _authenticationServiceHandler;

        public AuthenticationApiController(IAuthenticationServiceHandler authenticationServiceHandler)
        {
            _authenticationServiceHandler = authenticationServiceHandler;
        }

        [HttpGet]
        public IHttpActionResult RequestClientToken(string clientKey, int shopId)
        {
            return ExecuteInit("RequestClientToken", () => _authenticationServiceHandler.RequestClientToken(clientKey, shopId));
        }
    }
}