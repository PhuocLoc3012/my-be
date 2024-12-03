using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.SeedConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole
                {
                    Id = new Guid("c4fe1c86-da58-4570-a221-600153ca9038"),
                    Name = "Guess",
                    NormalizedName = "GUESS",
                    Description = "The visistor role for the user",
                },
                new ApplicationRole
                {
                    Id = new Guid("c4fe1c86-da58-4570-a221-600153ca9037"),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    Description = "The user buy the products",
                },
                new ApplicationRole
                {
                    Id = new Guid("c4fe1c86-da58-4570-a221-600153ca9068"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "The user manage the system",
                }

            );
        }
    }
}
