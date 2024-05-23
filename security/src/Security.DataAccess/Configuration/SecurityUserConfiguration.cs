using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    internal sealed class SecurityUserConfiguration<TUser, TIdentifier> : IEntityTypeConfiguration<TUser>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public void Configure(EntityTypeBuilder<TUser> builder)
        {
            builder.HasKey(user => user.ID);

            builder.Property(user => user.ID).IsRequired();
            builder.Property(user => user.UserName).IsRequired();
            builder.Property(user => user.Email).IsRequired();
            builder.Property(user => user.PhoneNumber);
            builder.Property(user => user.IsEmailConfirmed).IsRequired();
            builder.Property(user => user.IsPhoneNumberConfirmed).IsRequired();
            builder.Property(user => user.NormalizedEmail).IsRequired();
            builder.Property(user => user.NormalizedUserName).IsRequired();
        }
    }
}