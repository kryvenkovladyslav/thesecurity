using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using Security.DataAccess.Configuration;
using System;

namespace Security.DataAccess
{
    public abstract class SecurityDatabaseContext<TSecurityUser, TSecurityRole, TSecurityUserClaim, TSecurityUserRole, TSecurityRoleClaim, TIdentifier> : DbContext
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
        where TSecurityUser : SecurityUser<TIdentifier>
        where TSecurityUserClaim : SecurityClaim<TIdentifier>
        where TSecurityUserRole : SecurityUserRole<TIdentifier>
        where TSecurityRoleClaim : SecurityRoleClaim<TIdentifier>
    {
        public SecurityDatabaseContext() { }

        protected SecurityDatabaseContextOptions SecurityDatabaseOptions { get; private init; }

        public SecurityDatabaseContext(SecurityDatabaseContextOptions securityDatabaseOptions)
        {
            this.SecurityDatabaseOptions = securityDatabaseOptions ?? throw new ArgumentNullException(nameof(securityDatabaseOptions));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SecurityUserConfiguration<TSecurityUser, TIdentifier>());
            modelBuilder.ApplyConfiguration(new SecurityUserClaimConfiguration<TSecurityUser, TSecurityUserClaim, TIdentifier>());

            modelBuilder.ApplyConfiguration(new SecurityRoleConfiguration<TSecurityRole, TIdentifier>());
            modelBuilder.ApplyConfiguration(new SecurityRoleClaimConfiguration<TSecurityRole, TSecurityRoleClaim, TIdentifier>());
            modelBuilder.ApplyConfiguration(new SecurityUserRoleConfiguration<TSecurityUser, TSecurityRole, TSecurityUserRole, TIdentifier>());
        }
    }
}