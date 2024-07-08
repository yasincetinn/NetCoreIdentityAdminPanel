using FluentValidation;
using NetCoreIdentityAdminPanel.Models.Admins.AppUsers.RequestModels;

namespace NetCoreIdentityAdminPanel.Models.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequestModel>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı alanı zorunludur");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email formatında giriş yapınız");
            RuleFor(x => x.Email).NotNull().WithMessage("Email alanı zorunludur");
        }

    }
}
