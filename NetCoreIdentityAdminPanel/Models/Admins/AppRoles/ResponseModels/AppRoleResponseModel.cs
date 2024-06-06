namespace NetCoreIdentityAdminPanel.Models.Admins.AppRoles.ResponseModels
{
    public class AppRoleResponseModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Checked { get; set; } //Kullanıcı benim gönderdiğim response'taki role sahip mi?
    }
}
