using Microsoft.EntityFrameworkCore;
using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using System;
using System.Collections.Generic;

namespace Rolo.Auth.Tests.Builders
{
    public class BuilderJwtContext
    {
        List<AuthUser> authUsers;
        private BuilderJwtContext()
        {
            this.authUsers = new List<AuthUser>();
        }

        public static BuilderJwtContext New()
        {
            return new BuilderJwtContext();
        }



        public ContextJwt Build()
        {
            var options = new DbContextOptionsBuilder<ContextJwt>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var context = new ContextJwt(options);

            authUsers.ForEach(x => context.AuthUser.Add(x));

            context.SaveChanges();
            return context;
        }

        internal BuilderJwtContext With(List<AuthUser> authUsers)
        {
            authUsers.ForEach(x => this.authUsers.Add(x));
            return this;
        }

        public DbContextOptions<ContextJwt> DummyOptions { get; } = new DbContextOptionsBuilder<ContextJwt>().Options;
    }
}
