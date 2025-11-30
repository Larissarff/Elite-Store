using AppEcommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppEcommerce.Infra.Data.Configurations
{
    public class ItemPedidoConfig : IEntityTypeConfiguration<ItemPedidoEntity>
    {
        public void Configure(EntityTypeBuilder<ItemPedidoEntity> builder)
        {
            builder.ToTable("ItensPedido");

            builder.HasKey(i => i.IdItemPedido);

            builder.Property(i => i.IdItemPedido)
                   .ValueGeneratedOnAdd();

            builder.Property(i => i.IdProduto)
                   .IsRequired();

            builder.Property(i => i.Quantidade)
                   .IsRequired();

            builder.Property(i => i.Subtotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }
}
