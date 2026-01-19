using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;
using Microsoft.EntityFrameworkCore;

namespace go_han.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectMember> ProjectMembers { get; set; } = null!;
        public DbSet<Division> Divisions { get; set; } = null!;
        public DbSet<TaskItem> TaskItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Index untuk Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seed Data untuk Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "admin" },
                new Role { Id = 2, RoleName = "employee" }
            );

            // --- SOLUSI CASCADE PATH ERROR ---

            // 1. Relasi Project -> Lead (User)
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Lead)
                .WithMany()
                .HasForeignKey(p => p.LeadId)
                .OnDelete(DeleteBehavior.Restrict); // Mengganti Cascade menjadi Restrict

            // 2. Relasi Project -> CoLead (User)
            modelBuilder.Entity<Project>()
                .HasOne(p => p.CoLead)
                .WithMany()
                .HasForeignKey(p => p.CoLeadId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. Relasi TaskItem -> ApprovedBy (User)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.ApprovedBy)
                .WithMany()
                .HasForeignKey(t => t.ApprovedById)
                .OnDelete(DeleteBehavior.NoAction);

            // 4. Relasi ProjectMember -> User
            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMemberships)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}