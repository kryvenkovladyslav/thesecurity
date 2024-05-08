using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Security.EntityFrameworkStores
{
    public class SecurityUserStore<TContext, TUser, TIdentifier> : SecurityBaseStore<TContext>, 
        IUserStore<TUser>,
        IQueryableUserStore<TUser>
        where TContext : DbContext
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        protected IdentityErrorDescriber ErrorDescriber { get; private init; }

        public IQueryable<TUser> Users => this.GetSet<TUser>().AsNoTracking();

        public SecurityUserStore(TContext context, IdentityErrorDescriber errorDescriber) : base(context) 
        {
            this.ErrorDescriber = errorDescriber ?? throw new ArgumentNullException(nameof(errorDescriber));
        }

        #region CRUD Implementation

        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            this.GetSet<TUser>().Add(user);

            var identityResult = await this.TrySaveChangesAsync(cancellationToken);
            return identityResult;
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            this.GetSet<TUser>().Remove(user);

            var identityResult = await this.TrySaveChangesAsync(cancellationToken);
            return identityResult;
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            this.GetSet<TUser>().Update(user);

            var identityResult = await this.TrySaveChangesAsync(cancellationToken);
            return identityResult;
        }

        #endregion

        #region Finding Implementation

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            var convertedIdentifier = this.ConvertIdentifierFromString<Guid>(userId);
            var requiredUser = this.GetSet<TUser>()
                .FirstOrDefaultAsync(user => user.ID.Equals(convertedIdentifier), cancellationToken);

            return requiredUser;
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
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
            ArgumentException.ThrowIfNullOrEmpty(userName, nameof(userName));
            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;

            return Task.CompletedTask;
        }

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

        #endregion        
    }
}