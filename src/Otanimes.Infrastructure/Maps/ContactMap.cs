using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otanimes.Domain.Entities;

namespace Otanimes.Infrastructure.Maps;

public class ContactMap() : BaseMap<Contact>(tableName: "Contact")
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(entity => entity.Email)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");
        
        builder.Property(entity => entity.PrimaryPhone)
            .IsRequired()
            .HasColumnType("VARCHAR(15)");
        
        builder.Property(entity => entity.SecondaryPhone)
            .IsRequired(false)
            .HasColumnType("VARCHAR(15)");
    }
}