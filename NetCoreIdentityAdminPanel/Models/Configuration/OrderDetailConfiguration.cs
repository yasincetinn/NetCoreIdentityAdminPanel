using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreIdentityAdminPanel.Models.Entities;

namespace NetCoreIdentityAdminPanel.Models.Configuration
{
    public class OrderDetailConfiguration : BaseConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.ID);

            builder.HasKey(x => new
            {
                x.OrderID,
                x.ProductID
            });
        }
    }
}
