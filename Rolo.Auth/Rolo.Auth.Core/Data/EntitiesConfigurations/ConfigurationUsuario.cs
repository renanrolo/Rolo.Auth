using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Core.Data.EntitiesConfigurations
{
    internal class ConfigurationUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.UsuarioId);
            builder.Property(x => x.UsuarioId).ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired();
        }
    }
}
