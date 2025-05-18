using Khadamat_SellerPortal.Domain.OfflineSellerAggregate;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khadamat_SellerPortal.Infrastructure.OfflineSellers.Persistence
{
    public class OfflineSellerConfigurations : IEntityTypeConfiguration<OfflineSeller>
    {
        public void Configure(EntityTypeBuilder<OfflineSeller> builder)
        {
            // TPT: Id is both PK and FK to Sellers.Id
            builder.ToTable("OfflineSellers");

            // Configure Id as FK to Seller.Id (shared primary key)
            builder.HasOne<Seller>()
                .WithOne()
                .HasForeignKey<OfflineSeller>(o => o.Id); // FK points to Seller.Id
        }
    }
}
