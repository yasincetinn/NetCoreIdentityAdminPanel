using FluentValidation;
using NetCoreIdentityAdminPanel.Models.ViewModels.AppUsers.PureVMs;

namespace NetCoreIdentityAdminPanel.Models.Validators
{
    public class UserSignInValidator : AbstractValidator<UserSignInRequestModel>
    {
        public UserSignInValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı alanı zorunludur.");

            RuleFor(x => x.Password).NotNull().WithMessage("Şifre alanı zorunludur");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("En az 8 karakter girilmelidir");
            RuleFor(x => x.Password).MaximumLength(16).WithMessage("En fazla 16 karakter girilebilir");
        }

    }
}
