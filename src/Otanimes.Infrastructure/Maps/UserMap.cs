using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otanimes.Domain.Entities;

namespace Otanimes.Infrastructure.Maps;

public class UserMap() : BaseMap<User>(tableName: "User")
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(entity => entity.Username)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(entity => entity.Password)
            .IsRequired()
            .HasColumnType("VARCHAR(250)");
        
        builder.Property(entity => entity.LastLoginDate)
            .IsRequired(false)
            .HasColumnType("DATETIME");
    }
}