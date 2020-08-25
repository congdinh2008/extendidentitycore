using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExtendIdentity.Data
{
    public class ExtendIdentityContext : IdentityDbContext<User>
    {
        public ExtendIdentityContext(DbContextOptions<ExtendIdentityContext> options)
            : base(options)
        {
        }

        public DbSet<ClassAdmin> ClassAdmins { get; set; }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<Trainer>().HasBaseType<IdentityUser>();
            builder.Entity<Candidate>().HasBaseType<IdentityUser>();
        }
    }

    public class User: IdentityUser
    {
        public string ExternalEmail { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Gender { get; set; }

        public string AuditTrail { get; set; }
    }

    public enum Status
    {
        WaitingForClass, Allocated, Dropout
    }

    public class ClassAdmin
    {
        [Key]
        [ForeignKey("User")]
        public string ClassAdminId { get; set; }
        public User User { get; set; }
    }

    public class Trainee
    {
        [Key]
        [ForeignKey("User")]
        public string TraineeId { get; set; }

        public User User { get; set; }

        public AllocationStatus AllocationStatus { get; set; }

        public string ForeignLanguage { get; set; }

        public string CV { get; set; }
    }

    public enum AllocationStatus
    {
        Allocated, NotAllocated
    }

    public class Trainer: IdentityUser
    {
        public int Experience { get; set; }
    }

    public class Candidate: IdentityUser
    {
        public int EntryTestPoint { get; set; }
    }
}
