using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class ContactFormTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsContactForm, ContactForm>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Message))
                .ForMember(d => d.Mobile, opt => opt.MapFrom(s => s.Mobile))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject));
        }
    }
}
