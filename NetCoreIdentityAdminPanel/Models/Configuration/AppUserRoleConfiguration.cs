using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreIdentityAdminPanel.Models.Entities;

namespace NetCoreIdentityAdminPanel.Models.Configuration
{
    public class AppUserRoleConfiguration : BaseConfiguration<AppUserRole>
    {
        public override void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            base.Configure(builder);
            
            builder.Ignore(x => x.ID);

            builder.HasKey(x => new
            {
                x.RoleId,
                x.UserId
            });
        }

    }
}
