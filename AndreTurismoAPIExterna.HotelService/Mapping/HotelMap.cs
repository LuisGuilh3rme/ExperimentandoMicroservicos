using AndreTurismoAPIExterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndreTurismoAPIExterna.HotelService.Mapping
{
    public class HotelMap : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotel", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Endereco)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);
        }
    }
}
