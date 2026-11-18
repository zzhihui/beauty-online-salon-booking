using System.Collections.Generic;
using App.Service.Contract.Entities.Shop;

namespace App.Web.ViewModels
{
    public class CustomerFeedbackViewModel : BaseViewModel
    {
        public List<WsContactForm> Feedbacks { get; set; }
    }
}