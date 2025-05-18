using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.OnlineSellers.Persistence
{
    public class OnlineSellerConfigurations : IEntityTypeConfiguration<OnlineSeller>
    {
        public void Configure(EntityTypeBuilder<OnlineSeller> builder)
        {
            // TPT: Id is both PK and FK to Sellers.Id
            builder.ToTable("OnlineSellers");

            // Configure Id as FK to Seller.Id (shared primary key)
            builder.HasOne<Seller>()
                .WithOne()
                .HasForeignKey<OnlineSeller>(o => o.Id); // FK points to Seller.Id

        }
    }

}
