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
        /// <summary>
        /// Default constructor
        /// </summary>
        public SecurityDatabaseContext() { }

        /// <summary>
        /// Provides configuration for the database
        /// </summary>
        protected SecurityDatabaseContextOptions SecurityDatabaseOptions { get; private init; }

        /// <summary>
        /// Constructor for initializing instances using <see cref="SecurityDatabaseContextOptions"/>
        /// </summary>
        /// <param name="securityDatabaseOptions">Provides configuration for the database</param>
        public SecurityDatabaseContext(SecurityDatabaseContextOptions securityDatabaseOptions)
        {
            this.SecurityDatabaseOptions = securityDatabaseOptions ?? throw new ArgumentNullException(nameof(securityDatabaseOptions));
        }

        /// <summary>
        /// Configures all entities for the database
        /// </summary>
        /// <param name="modelBuilder">Provides API for applying configuration for entities inside a database</param>
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