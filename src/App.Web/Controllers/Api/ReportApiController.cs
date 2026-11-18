using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Service.Contract.Entities.Reports;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class ReportApiController : ApiControllerBase
    {
        private readonly IReportServiceHandler _reportServiceHandler;

        public ReportApiController(IReportServiceHandler reportServiceHandler)
        {
            _reportServiceHandler = reportServiceHandler;
        }

        [HttpGet]
        public IHttpActionResult GetReport(WsReport report, int shopId)
        {
            return Execute("GetReport", shopId, () => _reportServiceHandler.GetReport(report,shopId));
        }
    }
}
