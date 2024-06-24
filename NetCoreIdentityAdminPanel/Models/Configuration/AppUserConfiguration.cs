using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreIdentityAdminPanel.Models.Entities;

namespace NetCoreIdentityAdminPanel.Models.Configuration
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);


            //AppUser'a hem IdentityUser'dan hem de IEntity'den ID propertleri var. Ancak SQL tarafı bunları aynı isim olarak görüp(case-insensitive) hata verecektir.
            builder.Ignore(x => x.ID); //Kendi ID'imizi ignore ediyoruz çünkü Identity'nin bütün sistemlerini kullanabilmek için onun Id'sini korumak zorundayız... (Aynı şey AppRole için geçerli)

            builder.HasOne(x => x.Profile).WithOne(x => x.AppUser).HasForeignKey<AppUserProfile>(x => x.ID);

            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired();

            builder.HasMany(x => x.Orders).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserID);
        }

    }
}
