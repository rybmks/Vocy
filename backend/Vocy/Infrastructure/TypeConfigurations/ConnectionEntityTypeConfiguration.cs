using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TypeConfigurations;

public class ConnectionEntityTypeConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.HasKey(c => c.Id);
        builder.OwnsMany(c => c.Members, m =>
        {
            m.ToTable("ConnectionUsers");

            m.Property(cu => cu.UserId);
            m.HasKey("ConnectionId", nameof(ConnectionUser.UserId));

            m.Property(cu => cu.Role).HasColumnType("connection_role");

            m.HasOne(cu => cu.User).WithMany().HasForeignKey(cu => cu.UserId);
        });
    }
}