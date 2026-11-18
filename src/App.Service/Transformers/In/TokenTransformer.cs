using App.Data.Entities.Authentication;
using App.Service.Contract.Entities.Authentication;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class TokenTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsToken,Token>()
                  .IgnoreAllUnmapped()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                  .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                  .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                  .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))
                  .ForMember(d => d.TokenKey, opt => opt.MapFrom(s => s.TokenKey))
                  ;
        }
    }
}