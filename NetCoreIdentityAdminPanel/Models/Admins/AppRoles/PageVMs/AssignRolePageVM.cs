using NetCoreIdentityAdminPanel.Models.Admins.AppRoles.ResponseModels;

namespace NetCoreIdentityAdminPanel.Models.Admins.AppRoles.PageVMs
{
    public class AssignRolePageVM
    {
        public List<AppRoleResponseModel> Roles { get; set; }

        public int UserID { get; set; }
        
    }
}
