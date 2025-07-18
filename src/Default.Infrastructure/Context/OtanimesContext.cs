using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Default.Domain.Entities;
using Default.Domain.Interfaces.Infrastructure;
using Default.Infrastructure.Maps;

namespace Default.Infrastructure.Context;

public class DefaultContext : DbContext, IUnitOfWork
{
    #region Constructors
    
    public DefaultContext()
    {
    }
    
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    #endregion
    
    #region Maps

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("default");
        modelBuilder.ApplyConfiguration(new AddressMap());
        modelBuilder.ApplyConfiguration(new ContactMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk is { IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade });

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        
        base.OnModelCreating(modelBuilder);
    }

    #endregion
    
    #region Conventions

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<decimal>()
            .HavePrecision(9, 6);

        configurationBuilder
            .Properties<string>()
            .HaveColumnType("varchar(100)");

        configurationBuilder
            .Properties<Enum>()
            .HaveConversion<string>()
            .HaveColumnType("varchar(50)");
    }

    #endregion
    
    #region Unit Of Work
    
    private void FormatEntity()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(entity => entity is { Entity: Entity, State: EntityState.Added or EntityState.Modified });

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                (entity.Entity as Entity)!.CreatedAtNow();

            (entity.Entity as Entity)!.UpdatedAtNow();
        }
    }

    public void BeginTransaction()
        => Database.BeginTransaction();

    public void Commit()
        => Database.CommitTransaction();

    public void Rollback()
        => Database.RollbackTransaction();

    public async Task BeginTransactionAsync()
        => await Database.BeginTransactionAsync();

    public async Task CommitAsync()
        => await Database.CommitTransactionAsync();

    public async Task RollbackAsync()
        => await Database.RollbackTransactionAsync();
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        FormatEntity();
        return await base.SaveChangesAsync(cancellationToken);
    }

    #endregion
}