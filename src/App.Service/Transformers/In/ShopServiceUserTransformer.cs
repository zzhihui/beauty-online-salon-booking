using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class ShopServiceUserTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsShopServiceUser, ShopServiceUser>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))

                .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId));
        }
    }
}
