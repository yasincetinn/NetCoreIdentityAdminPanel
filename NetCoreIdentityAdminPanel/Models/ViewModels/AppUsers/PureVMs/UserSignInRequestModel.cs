using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityAdminPanel.Models.ViewModels.AppUsers.PureVMs
{
    public class UserSignInRequestModel
    {
        [Required(ErrorMessage = "Kullanıcı ismi zorunludur.")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; } //Burası kişinin başlangıçta gitmek istediği adresi tutar.. Kişi eğer login olmadan bir adrese gitmeye çalışırsa ve o adres Authorization'a sahipse kişi engellenir ve Login'e atılır.. Login'den istenilen role sahip olduğunu kanıtlarsa tekrar ilk gitmek istediği adrese otomatik gönderilmesi daha iyi olur.


    }
}
