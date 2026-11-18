using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Service.Contract.Entities.Payment;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class PaymentApiController : ApiControllerBase
    {

        private readonly IPaymentServiceHandler _paymentServiceHandler;

        public PaymentApiController(IPaymentServiceHandler paymentServiceHandler)
        {
            _paymentServiceHandler = paymentServiceHandler;
        }


        [HttpGet]
        public IHttpActionResult LoadPaymentMonths(int shopId)
        {
            return Execute("LoadPaymentMonths", shopId, () => _paymentServiceHandler.LoadPaymentMonths(shopId));
        }


        [HttpGet]
        public IHttpActionResult LoadPaymentYears(int shopId)
        {
            return Execute("LoadPaymentYears", shopId, () => _paymentServiceHandler.LoadPaymentYears(shopId));
        }



        [HttpPost]
        public IHttpActionResult TakePayment(WsPayment payment, int shopId)
        {
            return Execute("TakePayment", shopId, () => _paymentServiceHandler.TakePayment(payment,shopId));
        }


        [HttpGet]
        public IHttpActionResult GetAllPaymentTypes(int shopId)
        {
            return Execute("GetAllPaymentTypes", shopId, () => _paymentServiceHandler.GetAllPaymentTypes(shopId));
        }

    }
}
