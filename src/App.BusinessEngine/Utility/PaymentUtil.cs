using System;
using System.Globalization;
using App.Data.Entities.Payment;
using Stripe;

namespace App.BusinessEngine.Utility
{
    public interface IPaymentUtil
    {
        PaymentInfo TakePayment(Payment paymentInfo);
    }


    public class PaymentUtil : IPaymentUtil
    {
        public PaymentInfo TakePayment(Payment paymentInfo)
        {
            var paymentResult = new PaymentInfo();

            try
            {
                var tokenId = GetTokenId(paymentInfo);


                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = Convert.ToInt32(paymentInfo.Amount * 100),
                    Currency = paymentInfo.Currency,
                    Description = paymentInfo.Description,
                    TokenId = tokenId
                };

                var chargeService = new StripeChargeService();
                var stripeCharge = chargeService.Create(myCharge);
                paymentResult.IsSuccess = true;
                paymentResult.PaymentRef = stripeCharge.Id;
            }
            catch (StripeException ex)
            {
                paymentResult.IsSuccess = false;
                paymentResult.ErrorMessage = ex.Message;
            }
            catch (System.Exception ex)
            {
                paymentResult.IsSuccess = false;
                paymentResult.ErrorMessage = ex.Message;
            }

            return paymentResult;
        }

        private string GetTokenId(Payment paymentInfo)
        {
            var myToken = new StripeTokenCreateOptions
            {

                CardCvc = paymentInfo.CardCvc.ToString(CultureInfo.CurrentCulture),
                CardExpirationMonth = paymentInfo.CardExpirationMonth,
                CardExpirationYear = paymentInfo.CardExpirationYear,
                CardName = paymentInfo.CardName,
                CardNumber = paymentInfo.CardNumber
            };

            var tokenService = new StripeTokenService();
            var stripeToken = tokenService.Create(myToken);

            return stripeToken.Id;

        }
    }
}