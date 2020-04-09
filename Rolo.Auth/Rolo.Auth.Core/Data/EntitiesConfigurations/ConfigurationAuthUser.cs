using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Core.Data.EntitiesConfigurations
{
    internal class ConfigurationAuthUser : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder.HasKey(x => x.AuthUserId);
            builder.Property(x => x.AuthUserId).ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired(false);
        }
    }
}
