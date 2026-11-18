using System;
using App.Service.Contract.Entities.Shop;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;

namespace App.Service.Services
{
    public interface IShopNewsServiceHandler
    {
        WsShopNews GetShopNews(int shopId);
        WsShopNews SaveNews(WsShopNews selectedShopNews, int shopId);
        WsShopNews UpdateNews(WsShopNews selectedShopNews, int shopId);
        WsShopNews RemoveShopNewsById(int id, int shopId);
    }

    [ServiceHandler]
    public class ShopNewsServiceHandler : ServiceBase, IShopNewsServiceHandler
    {
        public ShopNewsServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer)
            : base(validatorFactory, unitOfWork, transformer)
        {
        }

        public WsShopNews GetShopNews(int shopId)
        {
            throw new NotImplementedException();
        }

        public WsShopNews SaveNews(WsShopNews selectedShopNews, int shopId)
        {
            throw new NotImplementedException();
        }

        public WsShopNews UpdateNews(WsShopNews selectedShopNews, int shopId)
        {
            throw new NotImplementedException();
        }

        public WsShopNews RemoveShopNewsById(int id, int shopId)
        {
            throw new NotImplementedException();
        }
    }
}