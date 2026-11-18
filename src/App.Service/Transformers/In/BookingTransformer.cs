using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class BookingTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsBooking, Booking>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))

                .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.CustomerId))
                .ForMember(d => d.DateTimeSlot, opt => opt.MapFrom(s => s.DateTimeSlot))
                .ForMember(d => d.Ref, opt => opt.MapFrom(s => s.Ref))
                .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.ShopUserId, opt => opt.MapFrom(s => s.ShopUserId));
        }
    }
}
