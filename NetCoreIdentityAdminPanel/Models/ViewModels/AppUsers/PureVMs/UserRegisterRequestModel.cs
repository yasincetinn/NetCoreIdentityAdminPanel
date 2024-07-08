namespace NetCoreIdentityAdminPanel.Models.ViewModels.AppUsers.PureVMs
{
    public class UserRegisterRequestModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword  { get; set; }
    }
}
