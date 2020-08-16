using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Backend.Models.Models;

namespace Project_Backend.Models.Data
{
    public class MusicKnowDbContext : IdentityDbContext<AppUser>
    {
        public MusicKnowDbContext(DbContextOptions<MusicKnowDbContext> options)
        : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ApiDelete> ApiDeletes { get; set; }
        public DbSet<ApiLog> ApiLogs { get; set; }
        public DbSet<Quizzes> Quizzes { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<BonusPoints> BonusPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quizzes>().ToTable("Quizzes");
            modelBuilder.Entity<Questions>().ToTable("Questions");
            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Score>().ToTable("Scores");
            modelBuilder.Entity<ApiLog>().ToTable("ApiLogs");
            modelBuilder.Entity<ApiDelete>().ToTable("ApiDeletes");
            modelBuilder.Entity<BonusPoints>().ToTable("BonusPoints");
            base.OnModelCreating(modelBuilder);

        }
    }
}
