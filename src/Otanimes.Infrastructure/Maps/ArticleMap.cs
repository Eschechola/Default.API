using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otanimes.Domain.Entities;

namespace Otanimes.Infrastructure.Maps;

public class ArticleMap() : BaseMap<Article>(tableName: "Article")
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(entity => entity.Title)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(entity => entity.SubTitle)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");
        
        builder.Property(entity => entity.Content)
            .IsRequired()
            .HasColumnType("VARCHAR(MAX)");

        builder.Property(entity => entity.IsDraft)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(1);

        builder.Property(entity => entity.PublishedAt)
            .IsRequired(false)
            .HasColumnType("DATETIME");
        
        builder.Property(entity => entity.Slug)
            .IsRequired()
            .HasColumnType("VARCHAR(500)");

        builder.HasOne(entity => entity.Author)
            .WithMany()
            .HasForeignKey(entity => entity.AuthorId);
    }
}