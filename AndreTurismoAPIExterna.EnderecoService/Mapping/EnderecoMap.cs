using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndreTurismoAPIExterna.EnderecoService.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Logradouro)
                .HasColumnType("varchar(50)")
                .HasDefaultValue("");

            builder.Property(x => x.Numero)
                .HasColumnType("integer")
                .HasDefaultValue(0);

            builder.Property(x => x.Bairro)
                .HasColumnType("varchar(50)")
                .HasDefaultValue("");

            builder.Property(x => x.CEP)
                .HasColumnType("char(8)")
                .IsRequired();

            builder.Property(x => x.Complemento)
                .HasColumnType("varchar(50)")
                .HasDefaultValue("");

            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}
