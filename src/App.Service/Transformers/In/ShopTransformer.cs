using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class ShopTransformer: ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsShop, ShopInfo>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))

                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.MinOrder, opt => opt.MapFrom(s => s.MinOrder))
                .ForMember(d => d.DeliveryCharge, opt => opt.MapFrom(s => s.DeliveryCharge))
                .ForMember(d => d.ShopNumber, opt => opt.MapFrom(s => s.ShopNumber))
                .ForMember(d => d.ShopMobile, opt => opt.MapFrom(s => s.ShopMobile))
                .ForMember(d => d.Address1, opt => opt.MapFrom(s => s.Address1))
                .ForMember(d => d.Address2, opt => opt.MapFrom(s => s.Address2))
                .ForMember(d => d.ShopMobile, opt => opt.MapFrom(s => s.ShopMobile))
                .ForMember(d => d.ShopOpenHours, opt => opt.MapFrom(s => s.ShopOpenHours))
                .ForMember(d => d.IsOpen, opt => opt.MapFrom(s => s.IsOpen))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.TakePayment, opt => opt.MapFrom(s => s.TakePayment))
                .ForMember(d => d.ShopDeliveryOpenHours, opt => opt.MapFrom(s => s.ShopDeliveryOpenHours))
                .ForMember(d => d.GoogleMap, opt => opt.MapFrom(s => s.GoogleMap))
                .ForMember(d => d.Androidlink, opt => opt.MapFrom(s => s.Androidlink))
                .ForMember(d => d.IoSlink, opt => opt.MapFrom(s => s.IoSlink))
                .ForMember(d => d.Isactive, opt => opt.MapFrom(s => s.Isactive))
                ;
        }
    }
}