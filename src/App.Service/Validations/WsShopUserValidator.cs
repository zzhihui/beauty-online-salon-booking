using App.Service.Contract.Entities.Authentication;
using FluentValidation;

namespace App.Service.Validations
{
    public interface IWsShopUserValidator
    {

    }

    public enum WsShopUserValidatorRuleSet
    {
        LoginUser
    }
    
    public class WsShopUserValidator : AbstractValidator<WsShopUser>, IWsShopUserValidator
    {
        public WsShopUserValidator()
        {
            RuleFor(p => p.UserName).NotNull().WithMessage("Username cannot be null");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(p => p.UserName).NotEqual(p => p.Password).WithMessage("Username and password cannot be the same");
            RuleFor(p => p.Password).NotNull().WithMessage("password cannot be null");
            RuleFor(p => p.Password.Length).GreaterThan(6).WithMessage("Password must be more than 6 chars");
            RuleFor(p => p.Email).NotNull().WithMessage("password cannot be null");

            RuleSet(WsShopUserValidatorRuleSet.LoginUser.ToString(), () =>
            {
                RuleFor(p => p.UserName).NotNull().WithMessage("Username cannot be null");
                RuleFor(p => p.Password).NotNull().WithMessage("password cannot be null");

            });
        }
    }
}