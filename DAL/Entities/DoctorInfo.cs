using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DoctorInfo
{
    public int DoctorId { get; set; }

    public string? Degree { get; set; }

    public string? Specialization { get; set; }

    public int? ExperienceYears { get; set; }

    public string? DoctorAvatar { get; set; }

    public string Status { get; set; } = null!;

    public virtual User Doctor { get; set; } = null!;
}
