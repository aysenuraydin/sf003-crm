using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(nameof(Notification), "dbo");

        builder.Property(a => a.Description).IsRequired();

        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.IsRead).IsRequired();
    }
}