using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Examination
{
    public int ExamId { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public string? Result { get; set; }

    public int? Cd4Count { get; set; }

    public int? HivLoad { get; set; }

    public DateOnly? ExamDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User Patient { get; set; } = null!;
}
