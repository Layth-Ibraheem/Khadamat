using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using Khadamat_UserManagement.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khadamat_UserManagement.Infrastructure.RegisterApplications.Persistence
{
    public class RegisterApplicationConfigurations : IEntityTypeConfiguration<RegisterApplication>
    {
        public void Configure(EntityTypeBuilder<RegisterApplication> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.Password)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(r => r.RejectionReason)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.HandledByUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


            builder.Property(r => r.Status)
                   .HasConversion(applicationStatus => applicationStatus.Value,
                   value => ApplicationStatus.FromValue(value))
                   .IsRequired()
                   .HasComment(
                   $"{ApplicationStatus.Rejected.Value}: {ApplicationStatus.Rejected.Name}, " +
                   $"{ApplicationStatus.Approved.Value}: {ApplicationStatus.Approved.Name}, " +
                   $"{ApplicationStatus.Draft.Value}: {ApplicationStatus.Draft.Name}, ");


        }
    }
}
