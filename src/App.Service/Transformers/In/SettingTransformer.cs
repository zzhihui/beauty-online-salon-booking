using App.Data.Entities.Settings;
using App.Service.Contract.Entities.Settings;
using AutoMapper;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;

namespace App.Service.Transformers.In
{
    [Transformer]
    public class SettingTransformer : ITransformerMapper
    {
        public void Initialise()
        {
            Mapper.CreateMap<WsSetting, ServerSetting>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ShopId, opt => opt.MapFrom(s => s.ShopId))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => s.Updated))
                .ForMember(d => d.SettingKey, opt => opt.MapFrom(s => s.SettingKey))
                .ForMember(d => d.SettingValue, opt => opt.MapFrom(s => s.SettingValue))
                .ForMember(d => d.IsServer, opt => opt.MapFrom(s => s.IsServer))
                ;
        }
    }
}