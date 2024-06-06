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
      
        public string? ReturnUrl { get; set; }


    }
}
