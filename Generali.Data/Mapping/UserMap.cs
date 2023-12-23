using Generali.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> b)
        {
            // Primary key
            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var admin = new AppUser
            {
                Id = Guid.Parse("80F2E962-334F-443E-9B87-14F259DAFCD2"),
                UserName = "admin@generali.com",
                NormalizedUserName = "ADMIN@GENERALI.COM",
                Email = "admin@generali.com",
                NormalizedEmail = "ADMIN@GENERALI.COM",
                PhoneNumber = "+9054525258612",
                FirstLastName = "Burak Patat",
                SecurityStamp=Guid.NewGuid().ToString()
            };
            admin.PasswordHash = CreatePasswordHash(admin, "1234");

            b.HasData(admin);
        }
        private string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
