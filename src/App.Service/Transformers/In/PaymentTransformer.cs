using App.Data.Entities.Payment;
using App.Service.Contract.Entities.Payment;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class PaymentTransformer : ITransformerMapper
    {
        public void Initialise()
        {

            Mapper.CreateMap<WsPayment, Payment>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.CardNumber, opt => opt.MapFrom(s => s.CardNumber))
                .ForMember(d => d.CardExpirationYear, opt => opt.MapFrom(s => s.CardExpirationYear))
                .ForMember(d => d.CardExpirationMonth, opt => opt.MapFrom(s => s.CardExpirationMonth))
                .ForMember(d => d.CardName, opt => opt.MapFrom(s => s.CardName))
                .ForMember(d => d.CardCvc, opt => opt.MapFrom(s => s.CardCvc))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))
                ;
        }
    }
}