using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
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
            // Primary Key
            builder.HasKey(x => x.Id);

            // Value Object: SellerPersonalDetails (Owned Entity)
            builder.OwnsOne(x => x.PersonalDetails, pd =>
            {
                // Nested Value Object: SellerAddress
                pd.Property(x => x.FirstName).HasMaxLength(20).IsRequired(true).HasColumnName("FirstName");
                pd.Property(x => x.SecondName).HasMaxLength(20).IsRequired(true).HasColumnName("SecondName");
                pd.Property(x => x.LastName).HasMaxLength(20).IsRequired(true).HasColumnName("LastName");
                pd.Property(x => x.Email).HasMaxLength(250).IsRequired(true).HasColumnName("Email");
                pd.Property(x => x.NationalNo).HasMaxLength(50).IsRequired(true).HasColumnName("NationalNo");
                pd.Property(x => x.DateOfBirth).IsRequired(true).HasColumnName("DateOfBirth");

                // Nested Owned Entity: SellerAddress
                pd.OwnsOne(x => x.Address, a =>
                {
                    a.Property(x => x.Country).HasMaxLength(50).IsRequired(true).HasColumnName("Country");
                    a.Property(x => x.City).HasMaxLength(50).IsRequired(true).HasColumnName("City");
                    a.Property(x => x.Region).HasMaxLength(100).IsRequired(true).HasColumnName("Region");
                });
            });

            // PortfolioUrls (1-to-Many)
            builder.HasMany(x => x.PortfolioUrls)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // SocialMediaLinks (1-to-Many)
            builder.HasMany(x => x.SocialMediaLinks)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // WorkExperiences (1-to-Many)
            builder.HasMany(x => x.WorkExperiences)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Educations (1-to-Many)
            builder.HasMany(x => x.Educations)
                .WithOne()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Table configuration
            builder.ToTable("OnlineSellers");
        }
    }

}
