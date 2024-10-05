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

    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = null!;

    public string Company { get; set; } = null!;

    [Display(Name = "Location")]
    public string WorkLocation { get; set; } = null!;

    [Display(Name = "Start Date")]
    public DateOnly WorkexStartDate { get; set; }

    [Display(Name = "End Date")]
    public DateOnly? WorkexEndDate { get; set; }

    public string Responsibilities { get; set; } = null!;

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
    public DateOnly VolunteerStartDate { get; set; }

    [Display(Name = "Volunteer End Date")]
    public DateOnly? VolunteerEndDate { get; set; }

    [Display(Name = "Volunteer Responsibilities")]
    public string VolunteerResponsibilities { get; set; } = null!;
}
