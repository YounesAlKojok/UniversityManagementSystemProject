using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UniversityManagementSystem.Models;

public partial class Appoinment
{
    public int Id { get; set; }
    [Display(Name = "Appointment Date")]
    public DateTime Date { get; set; }
    [Display(Name = "Dean")]
    public int DeanId { get; set; }

    public int Duration { get; set; }

    public string? Reason { get; set; }
    [Display(Name = "Student")]
    public int StudentId { get; set; }

    // New - properties to include a property for the day of the week
    public DayOfWeek DayOfWeek => Date.DayOfWeek;

    public virtual Dean? Dean { get; set; } = null!;

    public virtual Student? Student { get; set; } = null!;


    // addon methods and new var
    [Display(Name = "State")]
    public bool IsSlotActive { get; set; } = true;

}
