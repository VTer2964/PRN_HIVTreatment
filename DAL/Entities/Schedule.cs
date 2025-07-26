using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int DoctorId { get; set; }

    public DateTime ScheduledTime { get; set; }

    public string? Room { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual User Doctor { get; set; } = null!;
}
