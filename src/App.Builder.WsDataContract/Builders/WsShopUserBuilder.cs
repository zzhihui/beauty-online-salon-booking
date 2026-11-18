using App.Service.Contract.Entities.Authentication;

namespace App.Builder.WsDataContract.Builders
{
    public class WsShopUserBuilder
    {
        private readonly WsShopUser _entity;

        public WsShopUserBuilder()
        {
            _entity = new WsShopUser
            {
                Id = 1,
                UserName = "Bingliang Chen",
            };

        }


        public WsShopUser Build()
        {
            return _entity;
        }
    }
}