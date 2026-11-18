using System;
using System.Collections.Generic;
using App.Service.Contract.Entities.Settings;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;

namespace App.Service.Services
{
    public interface ISettingServiceHandler
    {
        List<WsSetting> GetSystemSettings(int shopId);
        WsSetting UpdateSystemSettings(WsSetting setting, int shopId);
    }

    [ServiceHandler]
    public class SettingServiceHandler : ServiceBase, ISettingServiceHandler
    {
        public SettingServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer) : base(validatorFactory, unitOfWork, transformer)
        {
        }
        
        public List<WsSetting> GetSystemSettings(int shopId)
        {
            throw new NotImplementedException();
        }

        public WsSetting UpdateSystemSettings(WsSetting setting, int shopId)
        {
            throw new NotImplementedException();
        }
    }
}