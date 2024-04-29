using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    public abstract class SecurityDatabaseContext<TUser, TIdentifier> : DbContext
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public DbSet<TUser> Users { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}