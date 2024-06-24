﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentityAdminPanel.Models.Configuration;
using NetCoreIdentityAdminPanel.Models.Entities;

namespace NetCoreIdentityAdminPanel.Models.ContextClasses
{
    //Identity kullanacak iseniz IdentityDbContext class'indan miras almalısınız ve eğer 3'ten fazla Identity class'i customize ettiyseniz IdentityDbContext'in generic yapısının tüm tiplerini açıkça vermek zorundasınız.

    public class MyContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserConfiguration());

            builder.ApplyConfiguration(new AppRoleConfiguration());

            builder.ApplyConfiguration(new AppUserRoleConfiguration());

            builder.ApplyConfiguration(new ProfileConfiguration());

            builder.ApplyConfiguration(new CategoryConfiguration());

            builder.ApplyConfiguration(new ProductConfiguration());

            builder.ApplyConfiguration(new OrderConfiguration());

            builder.ApplyConfiguration(new OrderDetailConfiguration());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<AppUserProfile> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

    }

}
