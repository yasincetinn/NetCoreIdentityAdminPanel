using Microsoft.AspNetCore.Identity;
using NetCoreIdentityAdminPanel.Models.Enums;
using NetCoreIdentityAdminPanel.Models.Interfaces;

namespace NetCoreIdentityAdminPanel.Models.Entities
{

    //IdentityUser class'ı bir Identity class'ıdır. AspNetCore.Identity kütüphanesinden gelir. Bu sınıfı default kullanırsanız sınıf tabloya dönüştürülürken edineceği primary key string tipte olur. Bizim sistemimizde tablolarımızın primary key'i int olduğu için biz IdentityUser'in int olarak tanımlanmasını söylemeliyiz ki bu da IdentityUser sınıfını generic olarak kullanarak gerçekleştirir. 

    //Eğer siz Identity kütüphanesindeki tablo yapılarını şekillendirmek istemezseniz hiç AppUser class'ı açmadan işlemleri Identity'e bırakabilirsiniz.. Kendisi bir AspNetUsers isimli tablo açarak bu işlemi yapacaktır. Ancak bilmelisiniz ki eğer özel bir müdahale yapmazsanız tablonun primary key'i string olacaktır.


    //Bizim özellikle AppUser class'ı açmak isteğimiz Identity tarafındaki yapıları şekillendirmektir. (Kendi istediğimiz özel property'leri koymak, ilişkileri kendi tarafımızdaki class'lar ile sağlamak vs..) Dolayısıyla biz bu emri verdiğimiz zaman Identity kütüphanesi hem kendi yapısını kendi property'leri ile oluşturacak hem de bizim istediğimiz yapıları da içerisine entegre edecektir.

    public class AppUser : IdentityUser<int>, IEntity
    {
        public AppUser()
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
       
        public virtual AppUserProfile Profile { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
