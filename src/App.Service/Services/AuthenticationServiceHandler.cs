using App.BusinessEngine.BusinessEngine;
using App.Data.Entities.Authentication;
using App.Service.Contract.Entities.Authentication;
using App.Service.Transformers;
using Core.Common.Core;
using FluentValidation;

namespace App.Service.Services
{
    public interface IAuthenticationServiceHandler
    {
        WsToken RequestClientToken(string clientKey, int shopId);
    }
    
    public class AuthenticationServiceHandler : ServiceBase, IAuthenticationServiceHandler
    {
        private readonly IAuthenticationBusinessEngine _authenticationBusinessEngine;

        public AuthenticationServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer, IAuthenticationBusinessEngine authenticationBusinessEngine) : base(validatorFactory, unitOfWork, transformer)
        {
            _authenticationBusinessEngine = authenticationBusinessEngine;
        }

        public WsToken RequestClientToken(string clientKey, int shopId)
        {
            return Query(() =>
            {
                var tokenPoco = _authenticationBusinessEngine.RequestClientToken(clientKey, shopId);
                //Transform out 
                return Transformer.Transform<Token, WsToken>(tokenPoco);
            });
        }
    }
}