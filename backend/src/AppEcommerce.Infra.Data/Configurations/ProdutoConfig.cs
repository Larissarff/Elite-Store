using AppEcommerce.Domain.Entities;
using AppEcommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppEcommerce.Infra.Data.Configurations;

public class ProdutoConfig : IEntityTypeConfiguration<ProdutoEntity>
{
    public void Configure(EntityTypeBuilder<ProdutoEntity> b)
    {
        b.ToTable("Produto");

        b.HasKey(p => p.Id);
        b.Property(p => p.Id)
         .HasColumnName("IdProduto");

        b.Property(p => p.Nome)
         .IsRequired()
         .HasMaxLength(200);

        b.Property(p => p.Descricao)
         .HasMaxLength(2000);

        b.Property(p => p.Preco)
         .HasColumnType("decimal(18,2)")
         .IsRequired();

        b.Property(p => p.Tamanho)
         .HasConversion<int>()
         .IsRequired();

        b.Property(p => p.Estoque)
         .IsRequired();

        b.Property(p => p.Categoria)
         .IsRequired()
         .HasMaxLength(120);
    }
}
