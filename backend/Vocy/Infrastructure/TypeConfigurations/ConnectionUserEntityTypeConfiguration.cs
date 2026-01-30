using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TypeConfigurations;

public class ConnectionUserEntityTypeConfiguration : IEntityTypeConfiguration<ConnectionUser>
{
    public void Configure(EntityTypeBuilder<ConnectionUser> builder)
    {
        builder.ToTable("ConnectionUser");
        builder.Property(cu => cu.Role).HasColumnType("connection_role");
    }
}