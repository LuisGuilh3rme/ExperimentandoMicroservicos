using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndreTurismoAPIExterna.PacoteService.Mapping
{
    public class PacoteMap : IEntityTypeConfiguration<Pacote>
    {
        public void Configure(EntityTypeBuilder<Pacote> builder)
        {
            builder.ToTable("Pacote", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Hotel)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.Passagem)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(x => x.Cliente)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");
        }
    }
}
