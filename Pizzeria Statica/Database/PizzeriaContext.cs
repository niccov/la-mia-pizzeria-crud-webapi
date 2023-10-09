﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizzeria_Statica.Models;

namespace Pizzeria_Statica.Database
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizze { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
