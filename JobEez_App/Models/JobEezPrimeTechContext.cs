using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobEez_App.Models;

public partial class JobEezPrimeTechContext : DbContext
{
    public JobEezPrimeTechContext()
    {
    }

    public JobEezPrimeTechContext(DbContextOptions<JobEezPrimeTechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BuildResume> BuildResumes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=JobEez_PrimeTech; Encrypt=False; Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BuildResume>(entity =>
        {
            entity.HasKey(e => e.PersonalInfoId).HasName("PK__BuildRes__0F38C33121720112");

            entity.ToTable("BuildResume");

            entity.Property(e => e.PersonalInfoId).HasColumnName("personal_info_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CareerObjective).HasColumnName("career_objective");
            entity.Property(e => e.CertificationName)
                .HasMaxLength(255)
                .HasColumnName("certification_name");
            entity.Property(e => e.Company)
                .HasMaxLength(255)
                .HasColumnName("company");
            entity.Property(e => e.Degree)
                .HasMaxLength(255)
                .HasColumnName("degree");
            entity.Property(e => e.EdLocation)
                .HasMaxLength(255)
                .HasColumnName("ed_location");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.GraduationYear).HasColumnName("graduation_year");
            entity.Property(e => e.Institution)
                .HasMaxLength(255)
                .HasColumnName("institution");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("job_title");
            entity.Property(e => e.Language)
                .HasMaxLength(255)
                .HasColumnName("language");
            entity.Property(e => e.LinkedinUrl)
                .HasMaxLength(255)
                .HasColumnName("linkedin_url");
            entity.Property(e => e.Organization)
                .HasMaxLength(255)
                .HasColumnName("organization");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("photo_url");
            entity.Property(e => e.Proficiency)
                .HasMaxLength(50)
                .HasColumnName("proficiency");
            entity.Property(e => e.RelevantCoursesAchievements).HasColumnName("relevant_courses_achievements");
            entity.Property(e => e.Responsibilities).HasColumnName("responsibilities");
            entity.Property(e => e.SkillName)
                .HasMaxLength(255)
                .HasColumnName("skill_name");
            entity.Property(e => e.SkillType)
                .HasMaxLength(20)
                .HasColumnName("skill_type");
            entity.Property(e => e.VolunteerEndDate).HasColumnName("volunteer_end_date");
            entity.Property(e => e.VolunteerResponsibilities).HasColumnName("volunteer_responsibilities");
            entity.Property(e => e.VolunteerRole)
                .HasMaxLength(255)
                .HasColumnName("volunteer_role");
            entity.Property(e => e.VolunteerStartDate).HasColumnName("volunteer_start_date");
            entity.Property(e => e.WorkLocation)
                .HasMaxLength(255)
                .HasColumnName("work_location");
            entity.Property(e => e.WorkexEndDate).HasColumnName("workex_end_date");
            entity.Property(e => e.WorkexStartDate).HasColumnName("workex_start_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
