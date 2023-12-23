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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> b)
        {
            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            b.ToTable("AspNetUserRoles");

            b.HasData(new AppUserRole()
            {
                UserId = Guid.Parse("80F2E962-334F-443E-9B87-14F259DAFCD2"),
                RoleId = Guid.Parse("ACE7A373-53E1-4316-8F53-B401C808BA55")
            });
        }
    }
}
