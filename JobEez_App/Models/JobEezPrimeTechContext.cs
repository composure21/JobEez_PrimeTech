﻿using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobEez_App.Models
{
    public partial class JobEezPrimeTechContext : IdentityDbContext<AspNetUser>
    {
        public JobEezPrimeTechContext(DbContextOptions<JobEezPrimeTechContext> options)
            : base(options)
        {
        }
        public virtual DbSet<BuildResume> BuildResumes { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Build configuration from appsettings.json
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Get the connection string from the configuration
                var connectionString = config.GetConnectionString("JobEez_AppContextConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Keep the base call to retain Identity configuration
            base.OnModelCreating(modelBuilder);

            // Configure AspNetUser
            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
            });

            // Define the primary key for AspNetUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogins");
                entity.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            });

            // Configure AspNetUserClaim
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
                entity.HasKey(e => e.Id);
            });

            // Configure AspNetUserToken (define primary key)
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserTokens");
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }); // Set primary key
            });
            // Configure AspNetUserRole (define primary key)
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
                entity.HasKey(e => new { e.UserId, e.RoleId }); // Set primary key
            });

        

            // Configure BuildResume entity
            modelBuilder.Entity<BuildResume>(entity =>
            {
                entity.HasKey(e => e.PersonalInfoId); 
                entity.ToTable("BuildResume");

                entity.Property(e => e.PersonalInfoId).HasColumnName("personal_info_id");
                entity.Property(e => e.Address).HasMaxLength(255).HasColumnName("address");
                entity.Property(e => e.CareerObjective).HasColumnName("career_objective");
                entity.Property(e => e.CertificationName).HasMaxLength(255).HasColumnName("certification_name");

                entity.Property(e => e.JobTitle).HasMaxLength(255).HasColumnName("job_title");
                entity.Property(e => e.JobTitle1).HasMaxLength(255).HasColumnName("job_title1");
                entity.Property(e => e.JobTitle2).HasMaxLength(255).HasColumnName("job_title2");

                entity.Property(e => e.Company).HasMaxLength(255).HasColumnName("company");
                entity.Property(e => e.Company1).HasMaxLength(255).HasColumnName("company1");
                entity.Property(e => e.Company2).HasMaxLength(255).HasColumnName("company2");

                entity.Property(e => e.WorkLocation).HasMaxLength(255).HasColumnName("work_location");
                entity.Property(e => e.WorkLocation1).HasMaxLength(255).HasColumnName("work_location1");
                entity.Property(e => e.WorkLocation2).HasMaxLength(255).HasColumnName("work_location2");

                entity.Property(e => e.WorkerEndDate).HasColumnName("worker_end_date");
                entity.Property(e => e.WorkerEndDate1).HasColumnName("worker_end_date1");
                entity.Property(e => e.WorkerEndDate2).HasColumnName("worker_end_date2");

                entity.Property(e => e.WorkerStartDate).HasColumnName("worker_start_date");
                entity.Property(e => e.WorkerStartDate1).HasColumnName("worker_start_date1");
                entity.Property(e => e.WorkerStartDate2).HasColumnName("worker_start_date2");

                entity.Property(e => e.Responsibilities).HasColumnName("responsibilities");
                entity.Property(e => e.Responsibilities1).HasColumnName("responsibilities1");
                entity.Property(e => e.Responsibilities2).HasColumnName("responsibilities2");

                entity.Property(e => e.Degree).HasMaxLength(255).HasColumnName("degree");
                entity.Property(e => e.EdLocation).HasMaxLength(255).HasColumnName("ed_location");
                entity.Property(e => e.FullName).HasMaxLength(255).HasColumnName("full_name");
                entity.Property(e => e.GraduationYear).HasColumnName("graduation_year");
                entity.Property(e => e.Institution).HasMaxLength(255).HasColumnName("institution");

            

                entity.Property(e => e.Language).HasMaxLength(255).HasColumnName("language");
                entity.Property(e => e.LinkedinUrl).HasMaxLength(255).HasColumnName("linkedin_url");
                entity.Property(e => e.Organization).HasMaxLength(255).HasColumnName("organization");
                entity.Property(e => e.PhoneNumber).HasMaxLength(20).HasColumnName("phone_number");
                entity.Property(e => e.PhotoUrl).HasMaxLength(255).HasColumnName("photo_url");
                entity.Property(e => e.Proficiency).HasMaxLength(50).HasColumnName("proficiency");
                entity.Property(e => e.RelevantCoursesAchievements).HasColumnName("relevant_courses_achievements");
               
                entity.Property(e => e.SkillName).HasMaxLength(255).HasColumnName("skill_name");
                entity.Property(e => e.SkillType).HasMaxLength(20).HasColumnName("skill_type");
                entity.Property(e => e.VolunteerEndDate).HasColumnName("volunteer_end_date");
                entity.Property(e => e.VolunteerResponsibilities).HasColumnName("volunteer_responsibilities");
                entity.Property(e => e.VolunteerRole).HasMaxLength(255).HasColumnName("volunteer_role");
                entity.Property(e => e.VolunteerStartDate).HasColumnName("volunteer_start_date");
               
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

        }
    }
}
