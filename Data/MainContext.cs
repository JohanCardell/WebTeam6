using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTeam6.Services;

namespace WebTeam6.Data
{
    public class MainContext : IdentityDbContext<User>
    {

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGroup>()
                .HasKey(p => new { p.GroupId, p.UserId });


            modelBuilder.Entity<UserGroup>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gu => gu.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserGroup>()
               .HasOne(gu => gu.User)
               .WithMany(u => u.GroupsAsMember)
               .HasForeignKey(gu => gu.UserId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Group>()
              .HasOne(g => g.Owner)
              .WithMany(u => u.GroupsAsOwner)
              .HasForeignKey(g => g.OwnerId)
              //.IsRequired()
              .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.CreatorId)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Events)
                .HasForeignKey(e => e.GroupId)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>().HasOne(e => e.Group);
            modelBuilder.Entity<Message>().HasOne(e => e.Creator);
        }
    }
}