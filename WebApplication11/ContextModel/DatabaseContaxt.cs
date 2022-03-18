﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Models;

namespace WebApplication11.Data
{
    public class DatabaseContaxt : IdentityDbContext
    {
        public DatabaseContaxt(DbContextOptions<DatabaseContaxt> options) :
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<CartDataModel> Carts { get; set; }
        public DbSet<UserRoleModel> aspnetuserroles { get; set; }
        public DbSet<MembersModel> aspnetusers { get; set; }  
        public DbSet<ProjectRoleModel> aspnetroles { get; set; }

        // public DbSet<CartProductModel> cartProductModels { get; set; }                      













    }
}