using App.Data.Entities.Authentication;
using App.Service.Contract.Entities.Authentication;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class ShopUserTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsShopUser, ShopUser>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
               .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))

               .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.DisplayName))
               .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
               .ForMember(d => d.UserGroupId, opt => opt.MapFrom(s => s.UserGroupId))
               .ForMember(d => d.Pin, opt => opt.MapFrom(s => s.Pin))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
               .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName));
        }
    }
}