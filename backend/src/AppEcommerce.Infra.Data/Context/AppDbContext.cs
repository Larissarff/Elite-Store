using AppEcommerce.Domain.Entities;
using AppEcommerce.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AppEcommerce.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<PagamentoEntity> Pagamentos { get; set; } = null!;

    public DbSet<ProdutoEntity> ProdutoEntitys => Set<ProdutoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutoConfig());
        base.OnModelCreating(modelBuilder);
    }
}
