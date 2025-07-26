using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int ScheduleId { get; set; }

    public int DoctorId { get; set; }

    public string? Note { get; set; }

    public string? AppoinmentType { get; set; }

    public bool IsAnonymous { get; set; }

    public string Status { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User Patient { get; set; } = null!;

    public virtual Schedule Schedule { get; set; } = null!;
}
