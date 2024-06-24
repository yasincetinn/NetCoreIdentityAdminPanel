using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetCoreIdentityAdminPanel.Models.Enums;
using NetCoreIdentityAdminPanel.Models.Interfaces;
using System.Data;
using System.Security.Principal;

namespace NetCoreIdentityAdminPanel.Models.Entities
{

    //AppUserRole sınıfında Relational Properties isimlerine çok dikkat ediniz. Bunların hepsi Identity'nin istediği standartlara uygun verilmiştir. Çok dikkat edin eğer bu Relation'lara müdahale edecekseniz bizzat Identity standartlarına uyun..

    //Identity standartlarında normalde ilişkisel Property User, Role olarak istenir.UserId ve RoleId property'lerine gerek yoktur çünkü onlar miras olarak gelmektedir. Relational Property'leri de bu yüzden User ve Role olarak veririz.

    public class AppUserRole : IdentityUserRole<int>, IEntity
    {
        public AppUserRole()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Inserted;
        }

        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        //Relational Properties

        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }   
}
