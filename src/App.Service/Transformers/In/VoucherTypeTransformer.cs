using App.Data.Entities.Authentication;
using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class VoucherTypeTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsVoucherType, VoucherType>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
               .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
               .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))

               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.Desc, opt => opt.MapFrom(s => s.Desc))
               .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId));
        }
    }
}
