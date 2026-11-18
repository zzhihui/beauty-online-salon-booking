using System.Collections.Generic;
using App.Service.Contract.Entities.Shop;

namespace App.Web.ViewModels
{
    public class ServiceUserViewModel : BaseViewModel
    {
        public List<ServiceUser> ServiceUsers { get; set; }
    }

    public class ServiceUser
    {
        public int UserId { get; set; }
        public string UserDisplayName { get; set; }
        public List<Service> ServiceList { get; set; }
    }

    public class Service
    {
        public string ServiceName { get; set; }
        public bool IsAssignedToUser { get; set; }
    }
}