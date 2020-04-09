using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTeam6.Data;

namespace WebTeam6.Data
{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                           "https://localhost:8081",
                           "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                           databaseName: "RemoteTool");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Tool");
            modelBuilder.Entity<User>().ToContainer("Users");
            modelBuilder.Entity<Group>().ToContainer("Groups");
            modelBuilder.Entity<MemberGroup>().ToContainer("MemberGroups");
            modelBuilder.Entity<OwnerGroup>().ToContainer("OwnerGroups");


            modelBuilder.Entity<MemberGroup>().HasKey(mg => new { mg.MemberId, mg.GroupId });
            modelBuilder.Entity<OwnerGroup>().HasKey(og => new { og.OwnerId, og.GroupId });


            modelBuilder.Entity<MemberGroup>().HasOne<User>().WithMany(u => u.MemberGroups).HasForeignKey(mg => mg.MemberId);
            modelBuilder.Entity<MemberGroup>().HasOne<Group>().WithMany(g => g.MemberGroups).HasForeignKey(mg => mg.GroupId);

            modelBuilder.Entity<OwnerGroup>().HasOne<User>().WithMany(u => u.OwnerGroups).HasForeignKey(og => og.OwnerId);
            modelBuilder.Entity<OwnerGroup>().HasOne<Group>().WithMany(g => g.OwnerGroups).HasForeignKey(ug => ug.GroupId);
        }
    }
}

public class MemberGroup
{
    public Guid MemberId { get; set; }
    public virtual User Member { get; set; }
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
}
public class OwnerGroup
{
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
}