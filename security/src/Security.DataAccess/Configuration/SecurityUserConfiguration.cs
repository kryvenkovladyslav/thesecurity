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
            var userTable = builder.ToTable(SecurityUserConfigurationDefaults.SecurityUserTableName);

            userTable.HasKey(user => user.ID);
            
            userTable
                .Property(user => user.ID)
                .HasColumnName(SecurityUserConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            userTable
                .Property(user => user.UserName)
                .HasColumnName(SecurityUserConfigurationDefaults.UserNameColumnName)
                .IsRequired();

            userTable
                .Property(user => user.Email)
                .HasColumnName(SecurityUserConfigurationDefaults.EmailColumnName)
                .IsRequired();

            userTable
                .Property(user => user.PhoneNumber)
                .HasColumnName(SecurityUserConfigurationDefaults.PhoneNumberColumnName)
                .HasDefaultValue(null)
                .IsRequired();

            userTable.Property(user => user.IsEmailConfirmed)
                .HasColumnName(SecurityUserConfigurationDefaults.EmailConfirmedColumnName)
                .HasDefaultValue(false)
                .IsRequired();

            userTable
                .Property(user => user.IsPhoneNumberConfirmed)
                .HasColumnName(SecurityUserConfigurationDefaults.PhoneNumberConfirmedColumnName)
                .HasDefaultValue(false)
                .IsRequired();

            userTable
                .Property(user => user.NormalizedEmail)
                .HasColumnName(SecurityUserConfigurationDefaults.NormalizedEmailColumnName)
                .IsRequired();

            userTable
                .Property(user => user.NormalizedUserName)
                .HasColumnName(SecurityUserConfigurationDefaults.NormalizedUserNameColumnName)
                .IsRequired();

            userTable
              .Property(user => user.SecurityStamp)
              .HasColumnName(SecurityUserConfigurationDefaults.SecurityStampColumnName)
              .HasDefaultValue(null)
              .IsRequired(false);
        }
    }
}