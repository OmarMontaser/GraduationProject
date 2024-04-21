using IsFakeModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeRepository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            

            base.OnModelCreating(builder);

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<UserStatement> UserStatements { get; set; }
        public DbSet<UserRecord> UserRecords { get; set; }


    }

}
