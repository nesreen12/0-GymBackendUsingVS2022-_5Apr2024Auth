using GymBackendUsingVS2022.Entities;
using GymBackendUsingVS2022.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ProgramList> ProgramLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        

        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        
        public DbSet<Class> classes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey });




            // Configure the one-to-many relationship between User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

           

            // Configure the one-to-one relationship between User and Member
                modelBuilder.Entity<User>()
                .HasOne(u => u.Member)
                .WithOne(m => m.User)
                .HasForeignKey<Member>(m => m.UserId);

            // Configure the one-to-one relationship between User and Instructor
            modelBuilder.Entity<User>()
                .HasOne(u => u.Instructor)
                .WithOne(I => I.User)
                .HasForeignKey<Instructor>(I => I.UserId);

            // Configure the one-to-many relationship between ProgramList and WorkoutPlan
            modelBuilder.Entity<WorkoutPlan>()
                .HasOne(wp => wp.ProgramList)
                .WithMany(pl => pl.WorkoutPlans)
                .HasForeignKey(wp => wp.ProgramListId);

            // Configure the one-to-many relationship between Instructor and WorkoutPlan
            modelBuilder.Entity<WorkoutPlan>()
                .HasOne(wp => wp.Instructor)
                .WithMany(I => I.WorkoutPlans)
                .HasForeignKey(wp => wp.InstructorId);

            // Configure the one-to-many relationship between WorkoutPlan and Class
            modelBuilder.Entity<Class>()
                .HasOne(c => c.WorkoutPlan)
                .WithMany(wp => wp.Classes)
                .HasForeignKey(c => c.WorkoutPlanId);

            // Configure the many-to-many relationship between ProgramList and MembershipType
            modelBuilder.Entity<MembershipType>()
                .HasMany(mt => mt.ProgramLists)
                .WithMany(pl => pl.MembershipTypes)
                .UsingEntity(j => j.ToTable("MembershipTypeProgramList"));

            // Configure the one to many relationship between Membership and MembershipType
            modelBuilder.Entity<Membership>()
                .HasOne(m => m.MembershipType)
                .WithMany(mt => mt.Memberships)
                .HasForeignKey(m => m.MembershipTypeId);

            // Configure the one-to-one relationship between Membership and Members
            modelBuilder.Entity<Membership>()
                .HasOne(m => m.Member)
                .WithOne(m => m.Membership)
                .HasForeignKey<Membership>(m => m.MemberId);

            // Configure the many-to-many relationship between Member and Workoutplan
            // modelBuilder.Entity<Member>()
            //   .HasMany(m => m.WorkoutPlans)
            // .WithMany(wp => wp.Members)
            //.UsingEntity(j => j.ToTable("MemberWorkoutPlan"));

            //

            modelBuilder.Entity<Attendance>()
                .HasKey(a => a.AttendanceId);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.User)
                .WithMany(u => u.Attendances)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Class)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

        }




    }
}
