using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndreTurismoAPIExterna.ClienteService.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Telefone)
                .HasColumnType("varchar(9)");

            builder.Property(x => x.Endereco)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}
