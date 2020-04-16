using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTeam6.Services;

namespace WebTeam6.Data
{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Groups);

            modelBuilder.Entity<Group>().HasMany(g => g.Members);
            modelBuilder.Entity<Group>().HasOne(g => g.Owner);
        }
    }
}