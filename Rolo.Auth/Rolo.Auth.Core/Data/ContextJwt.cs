using Microsoft.EntityFrameworkCore;
using Rolo.Auth.Core.Data.EntitiesConfigurations;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Core.Data
{
    public class ContextJwt : DbContext
    {
        public ContextJwt(DbContextOptions<ContextJwt> options)
        : base(options)
        {
        }

        public DbSet<AuthUser> AuthUser { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigurationAuthUser());
            modelBuilder.ApplyConfiguration(new ConfigurationUsuario());
        }
    }
}
