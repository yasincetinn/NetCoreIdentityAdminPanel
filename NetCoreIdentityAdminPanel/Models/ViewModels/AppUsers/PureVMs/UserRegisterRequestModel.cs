using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityAdminPanel.Models.ViewModels.AppUsers.PureVMs
{
    public class UserRegisterRequestModel
    {
        [Required(ErrorMessage = "{0} alanı girilmesi zorunludur.")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Email formatında giriş yapınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="{0} zorunludur.")]
        [MinLength(3, ErrorMessage = "Minimum karakter girilmesi gereklidir.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor.")]
        public string ConfirmPassword  { get; set; }
    }
}
