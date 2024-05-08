using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Security.EntityFrameworkStores
{
    public class SecurityUserStore<TContext, TUser, TIdentifier> : SecurityBaseStore<TContext>, 
        IUserStore<TUser>
        where TContext : DbContext
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public SecurityUserStore(TContext context) : base(context) { }

        #region CRUD Implementation

        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            this.GetSet<TUser>().Add(user);
            await this.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            this.GetSet<TUser>().Remove(user);
            await this.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            this.GetSet<TUser>().Update(user);
            await this.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }

        #endregion

        #region Finding Implementation

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNullOrEmpty(userId, nameof(userId));
            cancellationToken.ThrowIfCancellationRequested();

            var convertedIdentifier = this.ConvertIdentifierFromString<Guid>(userId);
            var requiredUser = this.GetSet<TUser>()
                .FirstOrDefaultAsync(user => user.ID.Equals(convertedIdentifier), cancellationToken);

            return requiredUser;
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNullOrEmpty(normalizedUserName, nameof(normalizedUserName));
            cancellationToken.ThrowIfCancellationRequested();

            var requiredUser = this.GetSet<TUser>()
                .FirstOrDefaultAsync(user => user.NormalizedUserName == normalizedUserName, cancellationToken);

            return requiredUser;
        }

        #endregion

        #region Getters and Setters Implementation

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            var convertedIdentifier = this.ConvertIdentifierToString(user.ID);
            return Task.FromResult(convertedIdentifier);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNullOrEmpty(normalizedName, nameof(normalizedName));
            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNullOrEmpty(userName, nameof(userName));
            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;

            return Task.CompletedTask;
        }

        #endregion        
    }
}