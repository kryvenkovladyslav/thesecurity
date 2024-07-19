using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Security.EntityFrameworkStores
{
    public class SecurityBaseStore<TContext, TIdentifier, TUser, TClaim>
        where TContext : DbContext
        where TIdentifier : IEquatable<TIdentifier>
        where TUser : SecurityUser<TIdentifier>, new()
        where TClaim : SecurityClaim<TIdentifier>, new()
    {
        private bool disposed;

        private readonly TContext context;

        public SecurityBaseStore(TContext context)
        {
            this.disposed = false;
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.context.Dispose();
            this.disposed = true;
        }

        protected virtual void ThrowIfDisposed()
        {
            if (this.disposed)
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

        protected virtual TClaim CreateUserClaim(TUser user, Claim claim)
        {
            var securityClaim = new TClaim { UserID = user.ID };
            securityClaim.InitializeFromClaim(claim);
            return securityClaim;
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