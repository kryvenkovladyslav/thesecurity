using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Security.EntityFrameworkStores
{
    public class SecurityBaseStore<TContext>
        where TContext : DbContext
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