using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class ShopServiceTransformer: ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsShopService, ShopService>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                //.ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                //.ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))

                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Length, opt => opt.MapFrom(s => s.Length))
                .ForMember(d => d.MaxPerDay, opt => opt.MapFrom(s => s.MaxPerDay))
                .ForMember(d => d.PriceFrom, opt => opt.MapFrom(s => s.PriceFrom))
                .ForMember(d => d.PriceTo, opt => opt.MapFrom(s => s.PriceTo));
        }
    }
}
