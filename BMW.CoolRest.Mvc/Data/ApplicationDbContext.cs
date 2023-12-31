﻿using BMW.CoolRest.Mvc.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMW.CoolRest.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}