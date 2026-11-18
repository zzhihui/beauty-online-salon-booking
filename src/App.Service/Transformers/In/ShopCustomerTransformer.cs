using App.Data.Entities.Authentication;
using App.Service.Contract.Entities.Authentication;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class ShopCustomerTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsShopCustomer, ShopCustomer>()
                .IgnoreAllUnmapped()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))
                .ForMember(d => d.ContactName, opt => opt.MapFrom(s => s.ContactName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.StoreCredit, opt => opt.MapFrom(s => s.StoreCredit))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.OldPassword, opt => opt.MapFrom(s => s.OldPassword))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(d => d.Ranking, opt => opt.MapFrom(s => s.Ranking))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
                .ForMember(d => d.ClientToken, opt => opt.MapFrom(s => s.ClientToken))
                .ForMember(d => d.TokenExpire, opt => opt.MapFrom(s => s.TokenExpire));
        }
    }
}