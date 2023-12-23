using Generali.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> b)
        {
            // Primary key
            b.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            b.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.Name).HasMaxLength(256);
            b.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            b.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            b.HasData(new AppRole
            {
                Id = Guid.Parse("ACE7A373-53E1-4316-8F53-B401C808BA55"),
                Name="generaliadmin",
                NormalizedName="Generali Admin",
                ConcurrencyStamp=Guid.NewGuid().ToString()
            });
        }
    }
}
