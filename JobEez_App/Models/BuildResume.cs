using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobEez_App.Models;

public partial class BuildResume
{
    public int PersonalInfoId { get; set; }

    [Display(Name = "Full name")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public string? LinkedinUrl { get; set; }

    public string? PhotoUrl { get; set; } = null!;

    [Display(Name = "Career Objective")]
    public string CareerObjective { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string Institution { get; set; } = null!;

    [Display(Name = "Education")]
    public string EdLocation { get; set; } = null!;

    [Display(Name = "Graduation Year")]
    public int GraduationYear { get; set; }

    [Display(Name = "Course Achievements")]
    public string? RelevantCoursesAchievements { get; set; }
    // Work experience Region

    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = null!;

    [Display(Name = "Job Title")]
    public string? JobTitle1 { get; set; } = null!;
    [Display(Name = "Job Title")]
    public string? JobTitle2 { get; set; } = null!;

    [Display(Name = "Company")]
    public string Company { get; set; } = null!;

    [Display(Name = "Company")]
    public string? Company1 { get; set; } = null!;
    [Display(Name = "Company")]
    public string? Company2 { get; set; } = null!;

    [Display(Name = "Location")]
    public string WorkLocation { get; set; } = null!;
    [Display(Name = "Location")]
    public string? WorkLocation1 { get; set; } = null!;
    [Display(Name = "Location")]
    public string? WorkLocation2 { get; set; } = null!;

    [Display(Name = "Start Date")]
    public DateTime WorkerStartDate { get; set; }

    [Display(Name = "End Date")]
    public DateTime? WorkerEndDate { get; set; }

    [Display(Name = "Start Date")]
    public DateTime? WorkerStartDate1 { get; set; }

    [Display(Name = "End Date")]
    public DateTime? WorkerEndDate1 { get; set; }

    [Display(Name = "Start Date")]
    public DateTime? WorkerStartDate2 { get; set; }

    [Display(Name = "End Date")]
    public DateTime? WorkerEndDate2 { get; set; }

    [Display(Name = "Responsibilities")]
    public string Responsibilities { get; set; } = null!;
    [Display(Name = "Responsibilities")]
    public string? Responsibilities1 { get; set; } = null!;
    [Display(Name = "Responsibilities")]
    public string? Responsibilities2 { get; set; } = null!;

    [Display(Name = "Skill Name")]
    public string SkillName { get; set; } = null!;

    [Display(Name = "Skill Type")]
    public string SkillType { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Proficiency { get; set; } = null!;

    [Display(Name = "Certification Name")]
    public string CertificationName { get; set; } = null!;

    [Display(Name = "Volunteer Role")]
    public string VolunteerRole { get; set; } = null!;

    public string Organization { get; set; } = null!;

    [Display(Name = "Volunteer Start Date")]
    public DateTime VolunteerStartDate { get; set; }

    [Display(Name = "Volunteer End Date")]
    public DateTime? VolunteerEndDate { get; set; }

    [Display(Name = "Volunteer Responsibilities")]
    public string VolunteerResponsibilities { get; set; } = null!;

    [Required]
    public string UserId { get; set; }
}
