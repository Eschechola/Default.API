using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otanimes.Domain.Entities;

namespace Otanimes.Infrastructure.Maps;

public class AddressMap() : BaseMap<Address>(tableName: "Address")
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(entity => entity.ZipCode)
            .IsRequired()
            .HasColumnType("VARCHAR(8)");
        
        builder.Property(entity => entity.State)
            .IsRequired()
            .HasColumnType("VARCHAR(2)");
        
        builder.Property(entity => entity.City)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");
        
        builder.Property(entity => entity.Street)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(entity => entity.Number)
            .IsRequired()
            .HasColumnType("VARCHAR(15)");
        
        builder.Property(entity => entity.Complement)
            .IsRequired(false)
            .HasColumnType("VARCHAR(500)");
    }
}