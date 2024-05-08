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
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>,
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

        #region IUserEmailStore Implementation

        public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.IsEmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            user.IsEmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            var requiredUser = this.Users.FirstOrDefaultAsync(user => user.NormalizedEmail == normalizedEmail);
            return requiredUser;
        }

        public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            user.Email = normalizedEmail;
            return Task.CompletedTask;
        }

        #endregion

        #region IUserPhoneNumberStore Implementation

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.IsEmailConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken = default)
        {
            this.ThrowIfDisposed();
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            cancellationToken.ThrowIfCancellationRequested();

            user.IsPhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        #endregion
    }
}