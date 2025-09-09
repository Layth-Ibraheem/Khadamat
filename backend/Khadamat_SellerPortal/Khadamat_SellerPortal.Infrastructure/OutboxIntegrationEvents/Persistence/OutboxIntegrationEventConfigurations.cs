using Khadamat_SellerPortal.Infrastructure.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.OutboxIntegrationEvents.Persistence
{
    public class OutboxIntegrationEventConfigurations : IEntityTypeConfiguration<OutboxIntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<OutboxIntegrationEvent> builder)
        {
            builder
            .Property<int>("Id")
            .ValueGeneratedOnAdd();

            builder.HasKey("Id");

            builder.Property(o => o.EventName).HasMaxLength(255);

            builder.Property(o => o.EventContent);
        }
    }
}
