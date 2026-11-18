using System.Web.Http;
using System.Web.Http.Results;
using App.Web.Application.HttpAction;

namespace App.Web.Application.Extension
{
    public static class ApiControllerExtension
    {
        public static NotFoundPlainTextActionResult NotFound(ApiController controller, string message)
        {
            return new NotFoundPlainTextActionResult(controller.Request, message);
        }
    }
}