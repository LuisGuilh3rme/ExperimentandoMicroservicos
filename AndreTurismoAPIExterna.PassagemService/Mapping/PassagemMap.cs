using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndreTurismoAPIExterna.PassagemService.Mapping
{
    public class PassagemMap : IEntityTypeConfiguration<Passagem>
    {
        public void Configure(EntityTypeBuilder<Passagem> builder)
        {
            builder.ToTable("Passagem", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Origem)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.Destino)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.Cliente)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.Data)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);
        }
    }
}
