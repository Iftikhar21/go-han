using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Interface;
using go_han.Models;
using Microsoft.EntityFrameworkCore;

namespace go_han.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IPasswordUtils _passwordUtils;

        public AppDbContext(DbContextOptions<AppDbContext> options, IPasswordUtils passwordUtils) : base(options)
        {
            this._passwordUtils = passwordUtils;
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

            // Seed Data untuk Divisions
            modelBuilder.Entity<Division>().HasData(
                new Division { Id = 1, DivisionName = "Frontend Dev" },
                new Division { Id = 2, DivisionName = "Backend Dev" }
            );

            var password = _passwordUtils.HashPassword("password");

            // Seed Data untuk Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = password,
                    RoleId = 1,
                },
                new User
                {
                    Id = 2,
                    Username = "employee",
                    Email = "employee@example.com",
                    PasswordHash = password,
                    RoleId = 2,
                },
                new User
                {
                    Id = 3,
                    Username = "Edi",
                    Email = "Edi@example.com",
                    PasswordHash = password,
                    RoleId = 2,
                },
                new User
                {
                    Id = 4,
                    Username = "Kurniadi",
                    Email = "Kurniadi@example.com",
                    PasswordHash = password,
                    RoleId = 2,
                }
            );

            // Seed Data untuk Projects
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    ProjectName = "Project A",
                    Description = "Description A",
                    Status = "In Progress",
                    LeadId = 1,
                    CoLeadId = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                }
            );

            // Seed Data untuk ProjectMembers
            modelBuilder.Entity<ProjectMember>().HasData(
                new ProjectMember { Id = 1, ProjectId = 1, UserId = 1, DivisionId = 1 },
                new ProjectMember { Id = 2, ProjectId = 1, UserId = 2, DivisionId = 2 }
            );

            // Seed Data untuk TaskItems
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, ProjectId = 1, AssigneeId = 3, AssignerId = 1, Title = "Task A", Content = "Description A", Difficulty = "Easy", Status = 0 },
                new TaskItem { Id = 2, ProjectId = 1, AssigneeId = 3, AssignerId = 2, Title = "Task B", Content = "Description B", Difficulty = "Medium", Status = 0 },
                new TaskItem { Id = 3, ProjectId = 1, AssigneeId = 4, AssignerId = 1, Title = "Task C", Content = "Description C", Difficulty = "Hard", Status = 1 }
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

            // 5. Relasi Task -> User Assigner
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Assigner)
                .WithMany()
                .HasForeignKey(t => t.AssignerId)
                .OnDelete(DeleteBehavior.NoAction);

            // 6. Relasi Task -> User Assignee
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Assignee)
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}