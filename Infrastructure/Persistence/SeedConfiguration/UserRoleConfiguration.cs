using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.SeedConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData
            (
                new IdentityUserRole<Guid>
                {
                    UserId = new Guid("01938297-aec3-712c-92e5-e5fa42829506"),
                    RoleId = new Guid("c4fe1c86-da58-4570-a221-600153ca9068"),
                }
            );
        }
    }
}
