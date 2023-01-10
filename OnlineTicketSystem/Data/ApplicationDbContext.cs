using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineTicketSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }

        public DbSet<UserMail> UserMails { get; set; }

        public DbSet<CompanyInformation> CompanyInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Status>().HasData(
            //    new Status
            //    {
            //        StatusID = 1,
            //        StatusName = "Open"
            //    }
            //);
            //modelBuilder.Entity<Status>().HasData(
            //    new Status
            //    {
            //        StatusID = 2,
            //        StatusName = "Closed"
            //    }
            //);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
               new Status { StatusID = 1, StatusName = "Open" },
               new Status { StatusID = 2, StatusName = "Closed" },
               new Status { StatusID = 3, StatusName = "Re-Opened" },
               new Status { StatusID = 4, StatusName = "Pending" }

           );

           // Guid guid = new Guid();
           // modelBuilder.Entity<CompanyInformation>().HasData(
           //    new CompanyInformation { ComID = guid, CompanyName = "GTR" }
              

           //);


            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "Admin".ToUpper() });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-421f-86af-483d56fd7210", Name = "Super Admin", NormalizedName = "Super Admin".ToUpper() });


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-421f-86af-493d06fd7210", Name = "Company Admin", NormalizedName = "Company Admin".ToUpper() });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-421f-86af-483d87fd7210", Name = "User", NormalizedName = "User".ToUpper() });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-421f-86af-483d92fd7210", Name = "Agent", NormalizedName = "Agent".ToUpper() });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();


           
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9550d048cdb9", // primary key
                    UserName = "gtradmin@gmail.com",
                    Email = "gtradmin@gmail.com",
                    NormalizedUserName = "gtradmin@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Gtradmin@123"),
                    EmailConfirmed = true
                }
            );

            //ComID = "dab2bdac-fb20-4faa-a268-4f67a25c9e64"




            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-421f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9550d048cdb9"
                }
            );





        }




    }
}
