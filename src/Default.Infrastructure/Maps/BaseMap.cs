using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Default.Domain.Entities;

namespace Default.Infrastructure.Maps;

public abstract class BaseMap<TEntity>(string tableName) : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(tableName);
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(entity => entity.UpdatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");
    }
}