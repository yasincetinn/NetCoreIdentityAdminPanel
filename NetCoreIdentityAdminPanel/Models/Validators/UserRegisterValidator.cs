using FluentValidation;
using NetCoreIdentityAdminPanel.Models.ViewModels.AppUsers.PureVMs;

namespace NetCoreIdentityAdminPanel.Models.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterRequestModel>
    {

        public UserRegisterValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email alanı zorunludur!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen doğru bir email giriniz!");


            RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı adı zorunludur!");


            RuleFor(x => x.Password).NotNull().WithMessage("Şifre alanı zorunludur");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("En az 8 karakter girilmelidir.").MaximumLength(16).WithMessage("En fazla 16 karakter girilebilir");


            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor");
        }
    }
}
