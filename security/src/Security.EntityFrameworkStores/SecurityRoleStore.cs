using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Security.EntityFrameworkStores
{
    public class SecurityRoleStore<TContext, TSecurityRole, TSecurityRoleClaim, TIdentifier> :
        IRoleStore<TSecurityRole>,
        IRoleClaimStore<TSecurityRole>,
        IQueryableRoleStore<TSecurityRole>
        where TContext : DbContext
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
        where TSecurityRoleClaim : SecurityRoleClaim<TIdentifier>, new()
    {
        private readonly TContext context;

        public IQueryable<TSecurityRole> Roles { get; private init; }

        protected bool IsDisposed { get; private set; }

        protected IdentityErrorDescriber ErrorDescriber { get; private init; }

        public SecurityRoleStore(TContext context, IdentityErrorDescriber errorDescriber)
        {
            this.IsDisposed = false;
            this.context = context;
            this.ErrorDescriber = errorDescriber;
            this.Roles = this.GetSet<TSecurityRole>().AsNoTracking();
        }

        #region IRoleStore implementation

        public virtual async Task<IdentityResult> CreateAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));

            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            this.GetSet<TSecurityRole>().Add(role);

            var identityResult = await this.TrySaveChangesAsync(cancellationToken);
            return identityResult;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));

            this.GetSet<TSecurityRole>().Remove(role);

            var identityResult = await this.TrySaveChangesAsync(cancellationToken);
            return identityResult;
        }

        public virtual async Task<TSecurityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentException.ThrowIfNullOrWhiteSpace(roleId, nameof(roleId));

            var convertedIdentifier = this.ConvertIdentifierFromString<TIdentifier>(roleId);
            var role = await this.GetSet<TSecurityRole>().FindAsync([convertedIdentifier], cancellationToken);
            return role;
        }

        public virtual async Task<TSecurityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentException.ThrowIfNullOrWhiteSpace(normalizedRoleName, nameof(normalizedRoleName));

            var role = await this.GetSet<TSecurityRole>()
                .FirstOrDefaultAsync(role => role.NormalizedName == normalizedRoleName, cancellationToken);

            return role;
        }

        public virtual Task<string> GetNormalizedRoleNameAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public virtual Task<string> GetRoleIdAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));

            var convertedIdentifier = this.ConvertIdentifierToString(role.ID);
            return Task.FromResult(convertedIdentifier);
        }

        public virtual Task<string> GetRoleNameAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));

            return Task.FromResult(role.Name);
        }

        public virtual Task SetNormalizedRoleNameAsync(TSecurityRole role, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            ArgumentException.ThrowIfNullOrWhiteSpace(normalizedName, nameof(normalizedName));

            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public virtual Task SetRoleNameAsync(TSecurityRole role, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            ArgumentException.ThrowIfNullOrWhiteSpace(roleName, nameof(roleName));

            role.Name = roleName;
            return Task.CompletedTask;
        }

        public virtual async Task<IdentityResult> UpdateAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            cancellationToken.ThrowIfCancellationRequested();

            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            this.GetSet<TSecurityRole>().Update(role);

            var identityResult = await this.TrySaveChangesAsync(cancellationToken);
            return identityResult;
        }

        #endregion

        #region IRoleClaimStore Implementation

        public virtual async Task<IList<Claim>> GetClaimsAsync(TSecurityRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            this.ThrowIfDisposed();

            var requiredClaims = await this.GetSet<TSecurityRoleClaim>()
                .Where(roleClaim => roleClaim.RoleID.Equals(role.ID))
                .ToListAsync(cancellationToken);

            var claims = requiredClaims.Select(roleClaim => new Claim(roleClaim.Type, roleClaim.Value));

            return claims.ToList();
        }
        
        public async virtual Task AddClaimAsync(TSecurityRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            ArgumentNullException.ThrowIfNull(claim, nameof(claim));
            this.ThrowIfDisposed();

            var existingClaims = await this.GetSet<TSecurityRoleClaim>()
                .Where(roleClaim => roleClaim.RoleID.Equals(role.ID) && roleClaim.Value == claim.Value && roleClaim.Type == claim.Type)
                .ToListAsync();

            if (existingClaims.Count != 0)
            {
                return;
            }

            this.GetSet<TSecurityRoleClaim>().Add(new TSecurityRoleClaim
            {
                RoleID = role.ID,
                Type = claim.Type,
                Value = claim.Value
            });

            await this.TrySaveChangesAsync(cancellationToken);
        }

        public async virtual Task RemoveClaimAsync(TSecurityRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            ArgumentNullException.ThrowIfNull(claim, nameof(claim));
            this.ThrowIfDisposed();

            var existingClaims = await this.GetSet<TSecurityRoleClaim>()
                .Where(roleClaim => roleClaim.RoleID.Equals(role.ID) && roleClaim.Value == claim.Value && roleClaim.Type == claim.Type)
                .ToListAsync();

            if (existingClaims.Count == 0)
            {
                return;
            }

            this.GetSet<TSecurityRoleClaim>().RemoveRange(existingClaims);
            await this.TrySaveChangesAsync(cancellationToken);
        }

        #endregion
        private async Task<IdentityResult> TrySaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await this.SaveChangesAsync(cancellationToken);
                return IdentityResult.Success;
            }
            catch (Exception)
            {

                return IdentityResult.Failed(this.ErrorDescriber.StorageFailure());
            }
        }
        public virtual void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.context.Dispose();
            this.IsDisposed = true;
        }

        protected virtual void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        protected virtual TKey ConvertIdentifierFromString<TKey>(string id)
            where TKey : IEquatable<TKey>
        {
            if (id == null)
            {
                return default(TKey);
            }

            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        protected virtual string ConvertIdentifierToString<TKey>(TKey id)
            where TKey : IEquatable<TKey>
        {
            return TypeDescriptor.GetConverter(typeof(string)).ConvertToInvariantString(id);
        }

        protected virtual async Task SaveChangesAsync(CancellationToken token = default)
        {
            await this.context.SaveChangesAsync(token);
        }

        protected virtual DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return this.context.Set<TEntity>();
        }
    }
}
