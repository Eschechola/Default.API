using System;
using Otanimes.Domain.Interfaces;
using Otanimes.Domain.Validators;

namespace Otanimes.Domain.Entities;

public class Article : Entity, IAggregateRoot
{
    // EF
    public Author Author { get; private set; }

    protected Article()
    {
    }

    public Article(
        Guid authorId,
        string title,
        string subTitle,
        string content,
        string slug,
        bool isDraft = true,
        DateTime? publishedAt = null)
    {
        AuthorId = authorId;
        Title = title;
        SubTitle = subTitle;
        Content = content;
        Slug = slug;
        IsDraft = isDraft;
        PublishedAt = publishedAt;
        
        Validate();
    }

    public Guid AuthorId { get; private set; }
    public string Title { get; private set; }
    public string SubTitle { get; private set; }
    public string Content { get; private set; }
    public string Slug { get; private set; }
    public bool IsDraft { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    public void Validate()
        => base.Validate(new ArticleValidator(), this);
}