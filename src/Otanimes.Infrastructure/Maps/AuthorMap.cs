using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otanimes.Domain.Entities;

namespace Otanimes.Infrastructure.Maps;

public class AuthorMap() : BaseMap<Author>(tableName: "Author")
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(entity => entity.FirstName)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(entity => entity.LastName)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");
        
        builder.HasOne(entity => entity.Contact)
            .WithMany()
            .HasForeignKey(entity => entity.ContactId);
        
        builder.HasOne(entity => entity.Address)
            .WithMany()
            .HasForeignKey(entity => entity.AddressId);
    }
}