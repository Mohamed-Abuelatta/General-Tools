﻿
using Core.Domains.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tools.Models;

namespace Data.Contexts
{

    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}


        #region AppUser
        public virtual DbSet<AppUser> AppUsers { get; set; }
        #endregion

        #region Student
        public DbSet<Customer> customers { get; set; }
        #endregion

        
        private string dbConn = "Server=.;Database=MyTool;Trusted_Connection=True;TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(dbConn, o =>
                {
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public class SystemDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            private string dbConn = "Server=.;Database=MyTool;Trusted_Connection=True;TrustServerCertificate=True;";
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(dbConn);
                return new AppDbContext(optionsBuilder.Options);
            }
        }

    }

}
