using Khadamat_SellerPortal.Domain.Common.Entities.ProfileImageEntity;
using Khadamat_SellerPortal.Domain.OfflineSellerAggregate;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Sellers.Persistence
{
    public class SellerConfigurations : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Value Object: SellerPersonalDetails (Owned Entity)
            builder.OwnsOne(x => x.PersonalDetails, pd =>
            {
                pd.Property(x => x.FirstName).HasMaxLength(20).IsRequired().HasColumnName("FirstName");
                pd.Property(x => x.SecondName).HasMaxLength(20).IsRequired().HasColumnName("SecondName");
                pd.Property(x => x.LastName).HasMaxLength(20).IsRequired().HasColumnName("LastName");
                pd.Property(x => x.Email).HasMaxLength(250).IsRequired().HasColumnName("Email");
                pd.Property(x => x.NationalNo).HasMaxLength(50).IsRequired().HasColumnName("NationalNo");
                pd.Property(x => x.DateOfBirth).IsRequired().HasColumnName("DateOfBirth");

                // Nested Owned Entity: SellerAddress
                pd.OwnsOne(x => x.Address, a =>
                {
                    a.Property(x => x.Country).HasMaxLength(50).IsRequired().HasColumnName("Country");
                    a.Property(x => x.City).HasMaxLength(50).IsRequired().HasColumnName("City");
                    a.Property(x => x.Region).HasMaxLength(100).IsRequired().HasColumnName("Region");
                });
            });

            // Configure relationships (moved from OnlineSeller)
            builder.HasMany(x => x.PortfolioUrls)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProfileImage)
                .WithOne()
                .HasForeignKey<ProfileImage>(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.SocialMediaLinks)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.WorkExperiences)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Educations)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
