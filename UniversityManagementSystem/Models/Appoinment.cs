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



    //public static List<DateTime> GetAvailableSlots()
    //{
    //    var availableSlots = new List<DateTime>();
    //    var startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);

    //    // Loop from 12:00 to 16:00 (Monday to Thursday)
    //    for (int i = 0; i < 4; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            var slotTime = startTime.AddMinutes(i * 20 + j * 5);

    //            // Check if the slot is on Monday to Thursday
    //            if (slotTime.DayOfWeek >= DayOfWeek.Monday && slotTime.DayOfWeek <= DayOfWeek.Thursday)
    //            {
    //                availableSlots.Add(slotTime);
    //            }
    //        }
    //    }

    //    return availableSlots;
    //}