using App.Service.Contract.Entities.Authentication;
using FluentValidation;

namespace App.Service.Validations
{
    public interface IWsShopCustomerValidator
    {

    }

    public enum WsShopCustomerEnum
    {
        LoginCustomer,
        RegisterCustomer,
        UpdateCustomer
    }

    public class WsShopCustomerValidator: AbstractValidator<WsShopCustomer>, IWsShopCustomerValidator
    {
        public WsShopCustomerValidator()
        {
            RuleFor(p => p.Email).NotNull().WithMessage("Username cannot be null");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("Password cannot be null")
                .Length(6, 20).WithMessage("Password must be more between 6 and 20 characters");
            
            //RuleFor(p => p.Address).NotNull().WithMessage("Address cannot be null");
            RuleFor(p => p.PhoneNumber).NotNull().WithMessage("PhoneNumber cannot be null");
            RuleForLoginCustomer();
            RuleForRegisterCustomer();
            RuleForUpdateCustomer();
        }

        private void RuleForUpdateCustomer()
        {
            RuleSet(WsShopCustomerEnum.UpdateCustomer.ToString(), () =>
            {
                RuleFor(p => p.Password)
                    .NotNull().WithMessage("Password cannot be null")
                    .Length(6, 20).WithMessage("Password must be more between 6 and 20 characters")
                    .When(p=>!string.IsNullOrEmpty(p.OldPassword));

                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
                    .WithMessage("Passwords do not match")
                    .When(p => !string.IsNullOrEmpty(p.OldPassword));

                //RuleFor(p => p.Address).NotNull().WithMessage("Address cannot be null");
                RuleFor(p => p.PhoneNumber).NotNull().WithMessage("PhoneNumber cannot be null");
            });
        }

        private void RuleForRegisterCustomer()
        {
            RuleSet(WsShopCustomerEnum.RegisterCustomer.ToString(), () =>
            {
                RuleFor(p => p.Email)
                    .NotNull().WithMessage("Username cannot be null")
                    .EmailAddress().WithMessage("Please provide valid email address");

                RuleFor(p => p.Password)
                    .NotNull().WithMessage("Password cannot be null")
                    .Length(6, 20).WithMessage("Password must be more between 6 and 20 characters");

                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");

                //RuleFor(p => p.Address).NotNull().WithMessage("Address cannot be null");

                RuleFor(p => p.PhoneNumber).NotNull().WithMessage("PhoneNumber cannot be null");
                RuleFor(p => p.Id).Equal(0).WithMessage("Id must be null");
            });
        }

        private void RuleForLoginCustomer()
        {
            RuleSet(WsShopCustomerEnum.LoginCustomer.ToString(), () =>
            {
                RuleFor(p => p.Email).NotNull().WithMessage("Username cannot be null");
                RuleFor(p => p.Password).NotNull().WithMessage("password cannot be null");
                RuleFor(p => p.Password.Length).GreaterThan(6).WithMessage("Password must be more than 6 chars");
            });
        }
    }
}