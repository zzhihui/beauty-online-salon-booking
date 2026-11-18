using App.Data.Entities.Authentication;
using Core.Common.Utility;

namespace App.BusinessEngine.BusinessEngine
{
    public interface IAuthenticationBusinessEngine
    {
        Token RequestClientToken(string clientKey, int shopId);
    }
    
    public class AuthenticationBusinessEngine : BusinessEngineBase, IAuthenticationBusinessEngine
    {
        private readonly ITokenEncryptor _tokenEncryptor;

        public AuthenticationBusinessEngine(ITokenEncryptor tokenEncryptor)
        {
            _tokenEncryptor = tokenEncryptor;
        }

        public Token RequestClientToken(string clientKey, int shopId)
        {
            return Execute(() => new Token{ TokenKey = _tokenEncryptor.Encrypt($"{clientKey},{shopId}") });
        }
    }
}