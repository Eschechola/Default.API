using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Otanimes.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private readonly ICollection<string> _errors = [];
    
    public bool IsInvalid()
        => !IsValid();

    public bool IsValid()
        => _errors.Count == 0;

    public void CreatedAtNow()
        => CreatedAt = DateTime.UtcNow;

    public void UpdatedAtNow()
        => UpdatedAt = DateTime.UtcNow;
    
    public string ErrorsToString()
        => _errors.Aggregate("", (current, error) => current + error + " ");
    
    protected void Validate<TValidator, TEntity>(TValidator validator, TEntity obj)
        where TValidator : AbstractValidator<TEntity>
    {
        ClearErrors();
        var validation = validator.Validate(obj);

        if (validation.Errors.Count > 0)
            AddErrors(validation.Errors);
    }
    
    private void AddErrors(List<ValidationFailure> errors)
    {
        foreach (var error in errors)
            _errors.Add(error.ErrorMessage);
    }
    
    private void ClearErrors()
        => _errors.Clear();
}