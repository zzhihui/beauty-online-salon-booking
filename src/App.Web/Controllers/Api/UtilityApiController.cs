using System.Web.Http;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class UtilityApiController : ApiControllerBase
    {
        private readonly IUtilityServiceHandler _utilityServiceHandler;

        public UtilityApiController(IUtilityServiceHandler utilityServiceHandler)
        {
            _utilityServiceHandler = utilityServiceHandler;
        }


        [HttpGet]
        public IHttpActionResult SyncStatus(int shopId)
        {
            return Execute("SyncStatus", shopId, () => _utilityServiceHandler.SyncStatus(shopId));
        }
    }
}
